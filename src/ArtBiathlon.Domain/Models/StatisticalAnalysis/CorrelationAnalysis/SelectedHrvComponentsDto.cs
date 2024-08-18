using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Domain.Models.StatisticalAnalysis.CorrelationAnalysis;

public record SelectedHrvComponentsDto(
    HrvIndicator FirstIndicator,
    HrvIndicator SecondIndicator);