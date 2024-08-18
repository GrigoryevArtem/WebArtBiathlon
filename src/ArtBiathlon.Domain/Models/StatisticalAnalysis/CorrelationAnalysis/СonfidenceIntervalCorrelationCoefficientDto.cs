namespace ArtBiathlon.Domain.Models.StatisticalAnalysis.CorrelationAnalysis;

public record СonfidenceIntervalCorrelationCoefficientDto(
    double LowerLimit,
    double UpperLimit
);