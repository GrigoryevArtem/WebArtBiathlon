namespace ArtBiathlon.Domain.Models.StatisticalAnalysis.FactorAnalysis;

public record ComponentPredominantFactorValuePercentageDto(
    byte ComponentNumber,
    double PercentageValue);