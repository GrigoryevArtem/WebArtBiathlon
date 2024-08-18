using ArtBiathlon.Domain.Models.StatisticalAnalysis;

namespace ArtBiathlon.Api.Responses.V1.CorrelationAnalysis;

public record GetPairwiseDependenceCoefficientsMatrixResponse(MatrixDto MatrixDto);