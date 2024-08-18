using ArtBiathlon.Api.Requests.V1.Training;
using ArtBiathlon.Api.Responses.V1.Training;
using ArtBiathlon.Domain.Interfaces.Services.Training;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/training")]
public class TrainingController : ControllerBase
{
    private readonly ITrainingService _trainingService;

    public TrainingController(ITrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpGet("get-training-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingResponse> GeById(byte id, CancellationToken token)
    {
        var trainingModel = await _trainingService.GetTrainingByIdAsync(id, token);
        return new GetTrainingResponse(trainingModel);
    }

    [HttpPost("create-training")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AddTrainingRequest request, CancellationToken token)
    {
        await _trainingService.CreateTrainingAsync(request.TrainingModel, token);
        return Ok();
    }

    [HttpGet("get-trainings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingsResponse> Get(CancellationToken token)
    {
        var trainingModels = await _trainingService.GetTrainingsAsync(token);
        return new GetTrainingsResponse(trainingModels);
    }

    [HttpGet("get-display-trainings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingsDisplayResponse> GetDisplayTrainings(CancellationToken token)
    {
        var trainingDisplayModels = await _trainingService.GetTrainingsDisplayAsync(token);
        return new GetTrainingsDisplayResponse(trainingDisplayModels);
    }

    [HttpDelete("delete-training")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task DeleteTraining(byte id, CancellationToken token)
    {
        await _trainingService.DeleteTrainingAsync(id, token);
    }

    [HttpPut("update-training")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateTraining(UpdateTrainingRequest request, CancellationToken token)
    {
        await _trainingService.UpdateTrainingAsync(request.Id, request.TrainingModel, token);
    }
}