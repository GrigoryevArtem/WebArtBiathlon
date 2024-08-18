namespace ArtBiathlon.Domain.Models.StatisticalAnalysis.CorrelationAnalysis;

public record ScatterChartDto(
    double[] PointsX,
    double[] PointsY);