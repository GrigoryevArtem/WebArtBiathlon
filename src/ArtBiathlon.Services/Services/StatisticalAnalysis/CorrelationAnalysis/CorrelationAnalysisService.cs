using Accord.Math;
using ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.CorrelationAnalysis;
using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using ArtBiathlon.Domain.Models.StatisticalAnalysis.CorrelationAnalysis;
using ArtBiathlon.Services.Helpers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Normal = MathNet.Numerics.Distributions.Normal;

namespace ArtBiathlon.Services.Services.StatisticalAnalysis.CorrelationAnalysis;

internal class CorrelationAnalysisService : ICorrelationAnalysisService
{
    private readonly IServiceProvider _provider;
    private double[][] _correlationMatrix = Array.Empty<double[]>();

    public CorrelationAnalysisService(IServiceProvider provider)
    {
        _provider = provider.CreateScope().ServiceProvider;
    }

    public async Task<MatrixDto> GetPairwiseDependenceCoefficientsMatrix(HrvDto[] hrvIndicators,
        CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<HrvDto>>();
        foreach (var hrvIndicator in hrvIndicators)
            await validator.ValidateAndThrowAsync(hrvIndicator, token);

        var matrix = StatisticalAnalysisHelper.HrvDtoArrayToDimensionalArray(hrvIndicators);
        _correlationMatrix = CorrelationAnalysisHelper.GetPairwiseDependenceCoefficientsMatrix(matrix);
        return new MatrixDto(_correlationMatrix);
    }

    public double GetLinearPearsonCorrelationCoefficient(SelectedHrvComponentsDto selectedHrvComponentsDto)
    {
        var firstIndicatorIndex = (int)selectedHrvComponentsDto.FirstIndicator;
        var secondIndicatorIndex = (int)selectedHrvComponentsDto.SecondIndicator;
        return _correlationMatrix[firstIndicatorIndex][secondIndicatorIndex];
    }

    public СonfidenceIntervalCorrelationCoefficientDto CalculateCorrelationInterval(int n, double correlation,
        double confidenceLevel = 0.95)
    {
        var zValue = Normal.InvCDF(0, 1, (1 + confidenceLevel) / 2);
        var zTransform = 0.5 * Math.Log((1 + correlation) / (1 - correlation));
        var standardError = 1 / Math.Sqrt(n - 3);
        var lowerBoundZ = zTransform - zValue * standardError;
        var upperBoundZ = zTransform + zValue * standardError;
        var lowerBoundCorrelation = (Math.Exp(2 * lowerBoundZ) - 1) / (Math.Exp(2 * lowerBoundZ) + 1);
        var upperBoundCorrelation = (Math.Exp(2 * upperBoundZ) - 1) / (Math.Exp(2 * upperBoundZ) + 1);
        return new СonfidenceIntervalCorrelationCoefficientDto(lowerBoundCorrelation, upperBoundCorrelation);
    }

    public PointDto[] GetScatterChartPoints(SelectedHrvComponentsDto selectedHrvComponentsDto)
    {
        var firstIndicatorIndex = (int)selectedHrvComponentsDto.FirstIndicator;
        var secondIndicatorIndex = (int)selectedHrvComponentsDto.SecondIndicator;

        var pointsX = StatisticalAnalysisHelper.GetColumn(_correlationMatrix, firstIndicatorIndex).ToArray();
        var pointsY = StatisticalAnalysisHelper.GetColumn(_correlationMatrix, secondIndicatorIndex).ToArray();

        var points = pointsX.Zip(pointsY, (x, y) => new PointDto(x, y)).ToArray();
        return points;
    }

    public DistributionChartDto GetDistributionChart(double[] points)
    {
        points.Sort();
        var distributionChart = CorrelationAnalysisHelper.GetDistributionChart(points);
        return distributionChart;
    }
}