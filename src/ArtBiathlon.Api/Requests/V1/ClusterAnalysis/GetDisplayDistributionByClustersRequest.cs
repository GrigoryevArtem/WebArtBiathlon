using ArtBiathlon.Domain.Models.StatisticalAnalysis;

namespace ArtBiathlon.Api.Requests.V1.ClusterAnalysis;

public record GetDisplayDistributionByClustersRequest(
    int FirstComponent,
    int SecondComponent,
    MatrixDto Matrix);