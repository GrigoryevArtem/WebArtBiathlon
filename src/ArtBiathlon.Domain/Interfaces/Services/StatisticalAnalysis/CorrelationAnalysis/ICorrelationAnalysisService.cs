using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using ArtBiathlon.Domain.Models.StatisticalAnalysis.CorrelationAnalysis;

namespace ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.CorrelationAnalysis;

public interface ICorrelationAnalysisService
{
    Task<MatrixDto> GetPairwiseDependenceCoefficientsMatrix(HrvDto[] matrix, CancellationToken token);
    double GetLinearPearsonCorrelationCoefficient(SelectedHrvComponentsDto selectedHrvComponentsDto);
    PointDto[] GetScatterChartPoints(SelectedHrvComponentsDto selectedHrvComponentsDto);

    СonfidenceIntervalCorrelationCoefficientDto CalculateCorrelationInterval(int n, double correlation,
        double confidenceLevel);

    DistributionChartDto GetDistributionChart(double[] points);
}