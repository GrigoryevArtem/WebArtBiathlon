using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Services.TrainingsSchedule;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/training-schedule")]
public class TrainingsScheduleController : ControllerBase
{
    private readonly ITrainingsScheduleService _trainingsScheduleService;

    public TrainingsScheduleController(ITrainingsScheduleService trainingsScheduleService)
    {
        _trainingsScheduleService = trainingsScheduleService;
    }

    [HttpGet("get-trainings-schedule-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ModelDtoWithId<TrainingsScheduleDto>> GeById(long id, CancellationToken token)
    {
        return await _trainingsScheduleService.GetTrainingsScheduleByIdAsync(id, token);
    }

    [HttpPost("create-trainings-schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(TrainingsScheduleDto trainingsScheduleDto, CancellationToken token)
    {
        await _trainingsScheduleService.CreateTrainingsScheduleAsync(trainingsScheduleDto, token);
        return Ok();
    }

    [HttpGet("get-trainings-schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ModelDtoWithId<TrainingsScheduleDto>[]> Get(CancellationToken token)
    {
        return await _trainingsScheduleService.GetTrainingsSchedulesAsync(token);
    }

    [HttpDelete("delete-trainings-schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(long id, CancellationToken token)
    {
        await _trainingsScheduleService.DeleteTrainingsScheduleAsync(id, token);
    }

    [HttpPut("update-trainings-schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Update(long id, TrainingsScheduleDto trainingsScheduleDto, CancellationToken token)
    {
        await _trainingsScheduleService.UpdateTrainingScheduleAsync(id, trainingsScheduleDto, token);
    }
}