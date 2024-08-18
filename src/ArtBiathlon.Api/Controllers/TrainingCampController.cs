using ArtBiathlon.Api.Requests.V1.TrainingCamp;
using ArtBiathlon.Api.Responses.V1.TrainingCamp;
using ArtBiathlon.Domain.Interfaces.Services.TrainingCamp;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/training-camp")]
public class TrainingCampController : ControllerBase
{
    private readonly ITrainingCampService _trainingCampService;

    public TrainingCampController(ITrainingCampService trainingCampService)
    {
        _trainingCampService = trainingCampService;
    }

    [HttpGet("get-training-camp-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingCampResponse> GeById(long id, CancellationToken token)
    {
        var trainingCampModel = await _trainingCampService.GetTrainingCampByIdAsync(id, token);
        return new GetTrainingCampResponse(trainingCampModel);
    }

    [HttpPost("create-training-camp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AddTrainingCampRequest request, CancellationToken token)
    {
        await _trainingCampService.CreateTrainingCampAsync(request.TrainingModel, token);
        return Ok();
    }

    [HttpGet("get-training-camps")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingCampsResponse> Get(CancellationToken token)
    {
        var trainingCampsModel = await _trainingCampService.GetTrainingCampsAsync(token);
        return new GetTrainingCampsResponse(trainingCampsModel);
    }

    [HttpDelete("delete-training-camp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(long id, CancellationToken token)
    {
        await _trainingCampService.DeleteTrainingCampAsync(id, token);
    }

    [HttpPut("update-training-camp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Update(UpdateTrainingCampRequest request, CancellationToken token)
    {
        await _trainingCampService.UpdateTrainingCampAsync(request.Id, request.TrainingsModel, token);
    }
}