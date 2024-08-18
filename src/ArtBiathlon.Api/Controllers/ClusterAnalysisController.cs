using ArtBiathlon.Api.Requests.V1.ClusterAnalysis;
using ArtBiathlon.Api.Responses.V1.ClusterAnalysis;
using ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.ClusterAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/cluster-analysis")]
public class ClusterAnalysisController : ControllerBase
{
    private readonly IClusterAnalysisService _clusterAnalysisService;

    public ClusterAnalysisController(IClusterAnalysisService clusterAnalysisService)
    {
        _clusterAnalysisService = clusterAnalysisService;
    }

    [HttpPost("get-normalized-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetNormalizedDataResponse> GetNormalizedData(GetNormalizedDataRequest request,
        CancellationToken token)
    {
        var normalizedData = await _clusterAnalysisService.NormalizationDataByMinMaxScaler(request.HrvDtos, token);
        return new GetNormalizedDataResponse(normalizedData);
    }

    [HttpPost("cluster-analyze")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task ClusterAnalysis(ClusterAnalysisRequest request, CancellationToken token)
    {
        await _clusterAnalysisService.ClusterAnalysis(request.Matrix, request.KlusterCount, token);
    }

    [HttpPost("elbow-method")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ElbowMethodResponse> ElbowMethod(ElbowMethodRequest request, CancellationToken token)
    {
        var elbowMethodResult = await _clusterAnalysisService.ElbowMethod(request.Matrix, token);
        return new ElbowMethodResponse(elbowMethodResult);
    }

    [HttpPost("get-display-distribution-by-clusters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetDisplayDistributionByClustersResponse> GetDisplayDistributionByClusters(
        GetDisplayDistributionByClustersRequest request, CancellationToken token)
    {
        var displayDistributionByClusters =
            await _clusterAnalysisService.GetDisplayDistributionByClusters(
                request.FirstComponent,
                request.SecondComponent,
                request.Matrix,
                token);

        return new GetDisplayDistributionByClustersResponse(displayDistributionByClusters);
    }

    [HttpPost("get-distribution-by-clusters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetDistributionByClustersWithAverageInfoResponse> GetDistributionByClustersWithAverageInfo(
        GetDistributionByClustersWithAverageInfoRequest request,
        CancellationToken token)
    {
        var distributionByClustersWithAverageInfo =
            await _clusterAnalysisService.GetDistributionByClustersWithAverageInfo(request.HrvDtos, token);
        return new GetDistributionByClustersWithAverageInfoResponse(distributionByClustersWithAverageInfo);
    }

    [HttpPost("get-average-hrv")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetAverageHrvResponse> GetAverageHrv(GetAverageHrvRequest request, CancellationToken token)
    {
        var averageHrvResponse = await _clusterAnalysisService.GetAverageHrv(request.HrvDtos, token);
        return new GetAverageHrvResponse(averageHrvResponse);
    }
}