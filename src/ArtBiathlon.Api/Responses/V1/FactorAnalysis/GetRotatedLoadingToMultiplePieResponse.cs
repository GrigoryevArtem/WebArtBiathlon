using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Models.StatisticalAnalysis.FactorAnalysis;

namespace ArtBiathlon.Api.Responses.V1.FactorAnalysis;

public record GetRotatedLoadingToMultiplePieResponse(
    Dictionary<HrvIndicator, List<ComponentPredominantFactorValuePercentageDto>> RotatedLoadingToMultiplePie);