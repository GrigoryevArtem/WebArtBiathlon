using ArtBiathlon.Api.Requests.V1.FactorAnalysis;
using ArtBiathlon.Api.Responses.V1.FactorAnalysis;
using ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.FactorAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/factor-analysis")]
public class FactorAnalysisController : ControllerBase
{
    private readonly IFactorAnalysisService _factorAnalysisService;

    public FactorAnalysisController(IFactorAnalysisService factorAnalysisService)
    {
        _factorAnalysisService = factorAnalysisService;
    }

    [HttpPost("get-normalized-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetNormalizedDataResponse> GetNormalizedData(GetNormalizedDataRequest request,
        CancellationToken token)
    {
        var normalizedData = await _factorAnalysisService.NormalizationDataByMinMaxScaler(request.HrvDtos, token);
        return new GetNormalizedDataResponse(normalizedData);
    }

    [HttpPost("factor-analyze-initialization-fit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task FactorAnalyzeInitializationFit(FactorAnalyzeInitializationFitRequest request,
        CancellationToken token)
    {
        await _factorAnalysisService.InitializationFit(request.MatrixDto, token);
    }

    [HttpPost("factor-analyze-fit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task FactorAnalyzeFit(FactorAnalyzeFitRequest request, CancellationToken token)
    {
        await _factorAnalysisService.FactorAnalysisFit(request.MatrixDto, request.NumberFactors, token);
    }

    [HttpPost("get-bartlett-test")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<double> GetBartlettTestValue(GetBartlettTestValueRequest request, CancellationToken token)
    {
        return await _factorAnalysisService.GetBartlettTestValue(request.MatrixDto, token);
    }

    [HttpGet("get-kaizer-criterion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public double GetKaiserCriterionValue()
    {
        return _factorAnalysisService.GetKaiserCriterionValue();
    }

    [HttpGet("get-eigen-values")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public GetEigenValuesResponse GetEigenValues()
    {
        var eigenValues = _factorAnalysisService.GetEigenValues();
        return new GetEigenValuesResponse(eigenValues);
    }

    [HttpGet("get-factor-transformation-matrix")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public GetFactorTransformationMatrixResponse GetFactorTransformationMatrix()
    {
        var factorTransformationMatrix = _factorAnalysisService.GetFactorTransformationMatrix();
        return new GetFactorTransformationMatrixResponse(factorTransformationMatrix);
    }

    [HttpGet("get-factor-loading-matrix")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public GetFactorLoadingMatrixResponse GetFactorLoadingMatrix()
    {
        var factorLoadingMatrix = _factorAnalysisService.GetFactorLoadingMatrix();
        return new GetFactorLoadingMatrixResponse(factorLoadingMatrix);
    }

    [HttpGet("get-cumulative-variance-explained")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public GetCumulativeVarianceExplainedResponse GetCumulativeVarianceExplained()
    {
        var cumulativeVarianceExplained = _factorAnalysisService.GetCumulativeVarianceExplained();
        return new GetCumulativeVarianceExplainedResponse(cumulativeVarianceExplained);
    }

    [HttpGet("get-rotated-loading-to-multiple-pie")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public GetRotatedLoadingToMultiplePieResponse GetRotatedLoadingToMultiplePie()
    {
        var rotatedLoadingToMultiplePie = _factorAnalysisService.GetRotatedLoadingToMultiplePie();
        return new GetRotatedLoadingToMultiplePieResponse(rotatedLoadingToMultiplePie);
    }
}