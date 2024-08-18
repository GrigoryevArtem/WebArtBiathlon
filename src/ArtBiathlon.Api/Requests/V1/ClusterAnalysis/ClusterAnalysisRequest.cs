using ArtBiathlon.Domain.Models.StatisticalAnalysis;

namespace ArtBiathlon.Api.Requests.V1.ClusterAnalysis;

public record ClusterAnalysisRequest(
    MatrixDto Matrix,
    int KlusterCount);