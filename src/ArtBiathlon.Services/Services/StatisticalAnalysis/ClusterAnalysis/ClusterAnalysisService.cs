using ArtBiathlon.Domain.Exceptions.StatisticalAnalysis.ClusterAnalysis;
using ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.ClusterAnalysis;
using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using ArtBiathlon.Domain.Models.StatisticalAnalysis.ClusterAnalysis;
using ArtBiathlon.Services.Helpers;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtBiathlon.Services.Services.StatisticalAnalysis.ClusterAnalysis;

public class ClusterAnalysisService : IClusterAnalysisService
{
    private readonly ClusterAnalysisHelper _clusterAnalysisHelper = new();
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _provider;

    public ClusterAnalysisService(IConfiguration configuration, IServiceProvider provider)
    {
        _configuration = configuration;
        _provider = provider.CreateScope().ServiceProvider;
    }

    public async Task ClusterAnalysis(MatrixDto matrix, int klusterCount, CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<MatrixDto>>();
        await validator.ValidateAndThrowAsync(matrix, token);
        await _clusterAnalysisHelper.KMeansFit(matrix.Data, klusterCount);
    }

    public async Task<MatrixDto> NormalizationDataByMinMaxScaler(HrvDto[] hrvIndicators, CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<HrvDto>>();
        foreach (var hrvIndicator in hrvIndicators) await validator.ValidateAndThrowAsync(hrvIndicator, token);
        var matrix = StatisticalAnalysisHelper.NormalizationDataByMinMaxScaler(hrvIndicators);
        return new MatrixDto(matrix);
    }

    public async Task<DistributionByClustersDto> GetDistributionByClustersWithAverageInfo(HrvDto[] hrvIndicators,
        CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<HrvDto>>();
        foreach (var hrvIndicator in hrvIndicators) await validator.ValidateAndThrowAsync(hrvIndicator, token);
        var distributionByCluster = GetDistributionByClusters(hrvIndicators);
        var averageHrvByClusters = GetAverageClustersDictionary(distributionByCluster);
        return new DistributionByClustersDto(distributionByCluster, averageHrvByClusters);
    }

    public async Task<ElbowMethodDto> ElbowMethod(MatrixDto matrix, CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<MatrixDto>>();
        await validator.ValidateAndThrowAsync(matrix, token);
        var tolerance = _configuration.GetValue<double>("ClusterAnalysis:Tolerance");
        var maxIterations = _configuration.GetValue<int>("ClusterAnalysis:MaxIterations");
        return ClusterAnalysisHelper.ElbowMethod(matrix.Data, tolerance, maxIterations);
    }

    public async Task<HrvAverageDto> GetAverageHrv(HrvDto[] hrvIndicators, CancellationToken token)
    {
        await ValidateHrvIndicators(hrvIndicators, token);
        return StatisticalAnalysisHelper.GetHrvAverage(hrvIndicators);
    }

    public async Task<Dictionary<int, List<PointDto>>> GetDisplayDistributionByClusters(
        int firstComponent,
        int secondComponent,
        MatrixDto componentsMatrix,
        CancellationToken token
    )
    {
        var validator = _provider.GetService<IValidator<MatrixDto>>();
        await validator.ValidateAndThrowAsync(componentsMatrix, token);
        var components = componentsMatrix.Data;
        var clustersComponentsDictionary = new Dictionary<int, List<PointDto>>();

        var centroids = _clusterAnalysisHelper.Labels;
        if (centroids is null) throw new CentroidsNullReferenceException();

        for (var i = 0; i < centroids.Count; ++i)
        {
            var currentLabel = centroids[i];

            if (clustersComponentsDictionary.ContainsKey(currentLabel))
                clustersComponentsDictionary[currentLabel].Add(new PointDto((float)components[i][firstComponent],
                    (float)components[i][secondComponent]));
            else
                clustersComponentsDictionary.Add(currentLabel, new List<PointDto>
                {
                    new((float)components[i][firstComponent],
                        (float)components[i][secondComponent])
                });
        }

        return clustersComponentsDictionary;
    }

    private Dictionary<int, List<HrvDto>> GetDistributionByClusters(HrvDto[] hrvIndicators)
    {
        var centroids = _clusterAnalysisHelper.Labels;

        if (centroids is null) throw new CentroidsNullReferenceException();

        var clustersDictionary = new Dictionary<int, List<HrvDto>>();

        for (var i = 0; i < centroids.Count; ++i)
        {
            var currentLabel = centroids[i];

            if (clustersDictionary.ContainsKey(currentLabel))
                clustersDictionary[currentLabel].Add(hrvIndicators[i]);
            else
                clustersDictionary.Add(currentLabel, new List<HrvDto> { hrvIndicators[i] });
        }

        clustersDictionary = clustersDictionary
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, y => y.Value);

        return clustersDictionary;
    }

    private HrvAverageDto[] GetAverageClustersDictionary(Dictionary<int, List<HrvDto>> clustersDictionary)
    {
        return clustersDictionary
            .Select(x => StatisticalAnalysisHelper.GetHrvAverage(x.Value.ToArray()))
            .ToArray();
    }

    private async Task ValidateHrvIndicators(HrvDto[] hrvIndicators, CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<HrvDto>>();
        foreach (var hrvIndicator in hrvIndicators) await validator.ValidateAndThrowAsync(hrvIndicator, token);
    }
}