namespace ArtBiathlon.Domain.Models.StatisticalAnalysis.FactorAnalysis;

public record FactorLoadingDto(
    double[][] LoadingMatrix,
    double[] VarianceExplained);