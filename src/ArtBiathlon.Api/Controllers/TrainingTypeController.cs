using ArtBiathlon.Domain.Interfaces.Services.TrainingType;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingType;
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
    public async Task<ModelDtoWithId<TrainingTypeDto>> GeById(byte id, CancellationToken token)
    {
        return await _trainingTypeService.GetTrainingTypeByIdAsync(id, token);
    }
    
    [HttpPost("create-training-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(TrainingTypeDto trainingTypeDto, CancellationToken token)
    { 
        await _trainingTypeService.CreateTrainingTypeAsync(trainingTypeDto, token);
        return Ok();
    }
    
    [HttpGet("get-training-types")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ModelDtoWithId<TrainingTypeDto>[]> Get(CancellationToken token)
    {
        return await _trainingTypeService.GetTrainingTypesAsync(token);
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
    public async Task UpdateTrainingType(byte id, TrainingTypeDto trainingTypeDto, CancellationToken token)
    {
        await _trainingTypeService.UpdateTrainingType(id, trainingTypeDto, token);
    }
}