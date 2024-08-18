using ArtBiathlon.Api.Requests.V1.CorrelationAnalysis;
using ArtBiathlon.Api.Responses.V1.CorrelationAnalysis;
using ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.CorrelationAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/correlation-analysis")]
public class CorrelationAnalysisController : ControllerBase
{
    private readonly ICorrelationAnalysisService _correlationAnalysisService;

    public CorrelationAnalysisController(ICorrelationAnalysisService correlationAnalysisService)
    {
        _correlationAnalysisService = correlationAnalysisService;
    }

    [HttpPost("get-pairwise-dependence-coefficients-matrix")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetPairwiseDependenceCoefficientsMatrixResponse> GetPairwiseDependenceCoefficientsMatrix(
        GetPairwiseDependenceCoefficientsMatrixRequest request, CancellationToken token)
    {
        var pairwiseDependenceCoefficientsMatrix =
            await _correlationAnalysisService.GetPairwiseDependenceCoefficientsMatrix(request.HrvDtos, token);
        return new GetPairwiseDependenceCoefficientsMatrixResponse(pairwiseDependenceCoefficientsMatrix);
    }

    [HttpPost("get-linear-pearson-correlation-coefficient")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public double GetLinearPearsonCorrelationCoefficient(GetLinearPearsonCorrelationCoefficientRequest request)
    {
        return _correlationAnalysisService.GetLinearPearsonCorrelationCoefficient(request.SelectedHrvComponentsDto);
    }

    [HttpPost("get-scatter-chart-points")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public GetScatterChartPointsResponse GetScatterChartPoints(GetScatterChartPointsRequest request)
    {
        var scatterChartPoints =
            _correlationAnalysisService.GetScatterChartPoints(request.SelectedHrvComponentsDto);
        return new GetScatterChartPointsResponse(scatterChartPoints);
    }

    [HttpPost("get-distribution-chart-points")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public GetDistributionChartPointsResponse GetDistributionChartPoints(GetDistributionChartPointsRequest request)
    {
        var distributionChartPoints =
            _correlationAnalysisService.GetDistributionChart(request.Points);
        return new GetDistributionChartPointsResponse(distributionChartPoints);
    }
}