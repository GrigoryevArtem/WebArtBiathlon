using ArtBiathlon.Api.Requests.V1.TrainingsSchedule;
using ArtBiathlon.Api.Responses.V1.TrainingsSchedule;
using ArtBiathlon.Domain.Interfaces.Services.TrainingsSchedule;
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
    public async Task<GetTrainingsScheduleResponse> GeById(long id, CancellationToken token)
    {
        var trainingsSchedule =
            await _trainingsScheduleService.GetTrainingsScheduleAsync(id, token);
        return new GetTrainingsScheduleResponse(trainingsSchedule);
    }

    [HttpGet("get-trainings-schedule-by-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingsScheduleResponse> GeByDate(DateTimeOffset startDate, CancellationToken token)
    {
        var trainingsSchedule =
            await _trainingsScheduleService.GetTrainingsScheduleAsync(startDate, token);
        return new GetTrainingsScheduleResponse(trainingsSchedule);
    }

    [HttpPost("create-trainings-schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AddTrainingsScheduleRequest request, CancellationToken token)
    {
        await _trainingsScheduleService.CreateTrainingsScheduleAsync(request.TrainingsSchedule, token);
        return Ok();
    }

    [HttpGet("get-trainings-schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingsSchedulesResponse> Get(CancellationToken token)
    {
        var trainingsSchedulesModels =
            await _trainingsScheduleService.GetTrainingsSchedulesAsync(token);

        return new GetTrainingsSchedulesResponse(trainingsSchedulesModels);
    }

    [HttpGet("get-trainings-schedule-display")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingsSchedulesDisplayResponse> GetTrainingsSchedulesDisplay(CancellationToken token)
    {
        var trainingsSchedulesDisplayModels =
            await _trainingsScheduleService.GetTrainingsSchedulesDisplayAsync(token);

        return new GetTrainingsSchedulesDisplayResponse(trainingsSchedulesDisplayModels);
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
    public async Task Update(UpdateTrainingsScheduleRequest request, CancellationToken token)
    {
        await _trainingsScheduleService.UpdateTrainingScheduleAsync(request.Id, request.TrainingsSchedule, token);
    }
}