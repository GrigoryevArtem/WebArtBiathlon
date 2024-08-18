namespace ArtBiathlon.Domain.Models.StatisticalAnalysis.CorrelationAnalysis;

public record DistributionChartDto(
    PointDto[] Points,
    double MinValue,
    double MaxValue,
    double TickAmount);