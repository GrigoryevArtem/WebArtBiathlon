using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Api.Requests.V1.ClusterAnalysis;

public record GetDistributionByClustersWithAverageInfoRequest(HrvDto[] HrvDtos);