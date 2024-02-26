using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Services.TrainingCamp;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingsCamp;
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
    public async Task<ModelDtoWithId<TrainingsCampDto>> GeById(long id, CancellationToken token)
    {
        return await _trainingCampService.GetTrainingCampByIdAsync(id, token);
    }
    
    [HttpPost("create-training-camp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(TrainingsCampDto trainingCampDto, CancellationToken token)
    { 
        await _trainingCampService.CreateTrainingCampAsync(trainingCampDto, token);
        return Ok();
    }
    
    [HttpGet("get-training-camps")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ModelDtoWithId<TrainingsCampDto>[]> Get(CancellationToken token)
    {
        return await _trainingCampService.GetTrainingCampsAsync(token);
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
    public async Task Update(long id, TrainingsCampDto trainingsCampDto, CancellationToken token)
    {
        await _trainingCampService.UpdateTrainingCampAsync(id, trainingsCampDto, token);
    }
}