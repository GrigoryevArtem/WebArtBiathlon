using ArtBiathlon.Domain.Models.StatisticalAnalysis.FactorAnalysis;

namespace ArtBiathlon.Api.Responses.V1.FactorAnalysis;

public record GetEigenValuesResponse(EigenValuesDto[] EigenValues);