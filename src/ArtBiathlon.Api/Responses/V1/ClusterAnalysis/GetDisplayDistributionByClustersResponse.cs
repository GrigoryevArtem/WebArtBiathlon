using ArtBiathlon.Domain.Models.StatisticalAnalysis;

namespace ArtBiathlon.Api.Responses.V1.ClusterAnalysis;

public record GetDisplayDistributionByClustersResponse(Dictionary<int, List<PointDto>> DisplayDistributionByClusters);