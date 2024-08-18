using ArtBiathlon.Domain.Models.StatisticalAnalysis;

namespace ArtBiathlon.Api.Requests.V1.FactorAnalysis;

public record FactorAnalyzeFitRequest(
    MatrixDto MatrixDto,
    int NumberFactors);