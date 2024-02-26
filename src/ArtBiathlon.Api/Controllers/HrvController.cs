using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Services.Hrv;
using ArtBiathlon.Domain.Models.Hrv;
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
    public async Task<ActionResult> Create(HrvDto hrvDto, CancellationToken token)
    {
        await _hrvService.CreateHrvAsync(hrvDto, token);
        return Ok();
    }

    [HttpGet("get-hrv-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HrvDto>> GeById(long id, CancellationToken token)
    {
        return await _hrvService.GetHrvByIdAsync(id, token);
    }

    [HttpGet("get-hrvs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<HrvDto[]> Get(CancellationToken token)
    {
        return await _hrvService.GetHrvsAsync(token);
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
    public async Task Update(long id, HrvDto hrvDto, CancellationToken token)
    {
        await _hrvService.UpdateHrvAsync(id, hrvDto, token);
    }
}