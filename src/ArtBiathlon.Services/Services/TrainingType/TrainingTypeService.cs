using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.TrainingType;
using ArtBiathlon.Domain.Interfaces.Dal.TrianingType;
using ArtBiathlon.Domain.Interfaces.Services.TrainingType;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingType;


namespace ArtBiathlon.Services.Services.TrainingType;

internal class TrainingTypeService : ITrainingTypeService
{
    private readonly ITrainingTypeRepository _trainingTypeRepository;

    public TrainingTypeService(ITrainingTypeRepository trainingTypeRepository)
    {
        _trainingTypeRepository = trainingTypeRepository;
    }

    public async Task<byte> CreateTrainingTypeAsync(TrainingTypeDto trainingTypeDto, CancellationToken token)
    {
        try
        {
            return await _trainingTypeRepository.CreateTrainingTypeAsync(trainingTypeDto, token);
        }
        catch (TrainingTypeAlreadyExistsException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingTypeDto>> GetTrainingTypeByIdAsync(byte id, CancellationToken token)
    {
        try
        {
            return await _trainingTypeRepository.GetTrainingTypeByIdAsync(id, token);
        }
        catch (TrainingTypeNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingTypeDto>[]> GetTrainingTypesAsync(CancellationToken token)
    {
        try
        {
            return await _trainingTypeRepository.GetTrainingTypesAsync(token);
        }
        catch (TrainingTypesNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task DeleteTrainingTypeAsync(byte id, CancellationToken token)
    {
        try
        {
            await _trainingTypeRepository.DeleteTrainingTypeAsync(id, token);
        }
        catch (TrainingTypeNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task UpdateTrainingType(byte id, TrainingTypeDto trainingTypeDto, CancellationToken token)
    {
        try
        {
            await _trainingTypeRepository.UpdateTrainingType(id, trainingTypeDto, token);
        }
        catch (TrainingTypeNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }
}