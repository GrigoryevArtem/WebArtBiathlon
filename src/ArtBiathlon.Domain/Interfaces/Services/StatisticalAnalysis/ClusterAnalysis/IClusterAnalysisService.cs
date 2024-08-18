using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using ArtBiathlon.Domain.Models.StatisticalAnalysis.ClusterAnalysis;

namespace ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.ClusterAnalysis;

public interface IClusterAnalysisService
{
    public Task ClusterAnalysis(MatrixDto matrix, int klusterCount, CancellationToken token);
    public Task<MatrixDto> NormalizationDataByMinMaxScaler(HrvDto[] hrvIndicators, CancellationToken token);

    public Task<Dictionary<int, List<PointDto>>> GetDisplayDistributionByClusters(int firstComponent,
        int secondComponent,
        MatrixDto componentsMatrix, CancellationToken token);

    public Task<ElbowMethodDto> ElbowMethod(MatrixDto matrix, CancellationToken token);

    public Task<DistributionByClustersDto> GetDistributionByClustersWithAverageInfo(HrvDto[] hrvIndicators,
        CancellationToken token);

    public Task<HrvAverageDto> GetAverageHrv(HrvDto[] hrvIndicators, CancellationToken token);
}