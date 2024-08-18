using ArtBiathlon.Api.Requests.V1.TrainingType;
using ArtBiathlon.Api.Responses.V1.TrainingType;
using ArtBiathlon.Domain.Interfaces.Services.TrainingType;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/training-type")]
public class TrainingTypeController : ControllerBase
{
    private readonly ITrainingTypeService _trainingTypeService;

    public TrainingTypeController(ITrainingTypeService trainingTypeService)
    {
        _trainingTypeService = trainingTypeService;
    }

    [HttpGet("get-training-type-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingTypeResponse> GeById(byte id, CancellationToken token)
    {
        var trainingTypeModel = await _trainingTypeService.GetTrainingTypeByIdAsync(id, token);
        return new GetTrainingTypeResponse(trainingTypeModel);
    }

    [HttpPost("create-training-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AddTrainingTypeRequest request, CancellationToken token)
    {
        await _trainingTypeService.CreateTrainingTypeAsync(request.TrainingTypeModel, token);
        return Ok();
    }

    [HttpGet("get-training-types")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTrainingTypesResponse> Get(CancellationToken token)
    {
        var trainingTypesModel = await _trainingTypeService.GetTrainingTypesAsync(token);
        return new GetTrainingTypesResponse(trainingTypesModel);
    }

    [HttpDelete("delete-training-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task DeleteTrainingType(byte id, CancellationToken token)
    {
        await _trainingTypeService.DeleteTrainingTypeAsync(id, token);
    }

    [HttpPut("update-training-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateTrainingType(UpdateTrainingTypeRequest request, CancellationToken token)
    {
        await _trainingTypeService.UpdateTrainingType(request.Id, request.TrainingTypeModel, token);
    }
}