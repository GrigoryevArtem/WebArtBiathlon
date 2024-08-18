using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Domain.Models.StatisticalAnalysis.ClusterAnalysis;

public record DistributionByClustersDto(
    Dictionary<int, List<HrvDto>> DistributionByClusters,
    HrvAverageDto[] AverageHrvByClusters
);