using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Services.Training;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Training;
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
    public async Task<ModelDtoWithId<TrainingDto>> GeById(byte id, CancellationToken token)
    {
        return await _trainingService.GetTrainingByIdAsync(id, token);
    }
    
    [HttpPost("create-training")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(TrainingDto trainingDto, CancellationToken token)
    { 
        await _trainingService.CreateTrainingAsync(trainingDto, token);
        return Ok();
    }
    
    [HttpGet("get-trainings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ModelDtoWithId<TrainingDto>[]> Get(CancellationToken token)
    {
        return await _trainingService.GetTrainingsAsync(token);
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
    public async Task UpdateTraining(byte id, TrainingDto trainingDto, CancellationToken token)
    {
        await _trainingService.UpdateTrainingAsync(id, trainingDto, token);
    }
}