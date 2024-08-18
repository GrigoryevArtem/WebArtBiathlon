using ArtBiathlon.Domain.Models.StatisticalAnalysis.CorrelationAnalysis;

namespace ArtBiathlon.Api.Responses.V1.CorrelationAnalysis;

public record GetDistributionChartPointsResponse(
    DistributionChartDto DistributionChart);