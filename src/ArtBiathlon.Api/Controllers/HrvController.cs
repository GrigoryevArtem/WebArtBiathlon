using ArtBiathlon.Api.Requests.V1.Hrv;
using ArtBiathlon.Api.Responses.V1.Hrv;
using ArtBiathlon.Domain.Interfaces.Services.Hrv;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/hrv-indicators")]
public class HrvController : ControllerBase
{
    private readonly IHrvService _hrvService;

    public HrvController(IHrvService hrvService)
    {
        _hrvService = hrvService;
    }

    [HttpPost("create-hrv")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AddHrvRequest request, CancellationToken token)
    {
        await _hrvService.CreateHrvAsync(request.HrvModel, token);
        return Ok();
    }

    [HttpGet("get-hrv-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetHrvResponse> GeById(long id, CancellationToken token)
    {
        var hrvModel = await _hrvService.GetHrvByIdAsync(id, token);
        return new GetHrvResponse(hrvModel);
    }

    [HttpGet("get-hrvs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetHrvsResponse> Get(CancellationToken token)
    {
        var hrvModels = await _hrvService.GetHrvsAsync(token);
        return new GetHrvsResponse(hrvModels);
    }

    [HttpPost("get-hrvs-by-biathlete-ids-and-camp-ids-and-type-days")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetHrvsResponse> GetByBiathleteIdsAndCampIdsAndTypeDays(
        HrvFilterByBiathleteIdAndCampIdRequest request,
        CancellationToken token)
    {
        var hrvModels = await _hrvService.GetHrvsAsync(
            request.UserInfoId,
            request.CampId,
            request.DayType,
            token);
        return new GetHrvsResponse(hrvModels);
    }

    [HttpDelete("delete-hrv")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(long id, CancellationToken token)
    {
        await _hrvService.DeleteHrvAsync(id, token);
    }

    [HttpPut("update-hrv")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Update(UpdateHrvRequest request, CancellationToken token)
    {
        await _hrvService.UpdateHrvAsync(request.Id, request.HrvModel, token);
    }
}