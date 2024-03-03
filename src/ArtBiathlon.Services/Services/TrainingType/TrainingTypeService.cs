using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.TrainingType;
using ArtBiathlon.Domain.Interfaces.Dal.TrianingType;
using ArtBiathlon.Domain.Interfaces.Services.TrainingType;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingType;
using FluentValidation;


namespace ArtBiathlon.Services.Services.TrainingType;

internal class TrainingTypeService : ITrainingTypeService
{
    private readonly ITrainingTypeRepository _trainingTypeRepository;
    private readonly IValidator<TrainingTypeDto> _validator;
    public TrainingTypeService(ITrainingTypeRepository trainingTypeRepository, IValidator<TrainingTypeDto> validator)
    {
        _trainingTypeRepository = trainingTypeRepository;
        _validator = validator;
    }

    public async Task<byte> CreateTrainingTypeAsync(TrainingTypeDto trainingTypeDto, CancellationToken token)
    {
        try
        { 
            await _validator.ValidateAndThrowAsync(trainingTypeDto, cancellationToken: token);
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
            await _validator.ValidateAndThrowAsync(trainingTypeDto, cancellationToken: token);
            await _trainingTypeRepository.UpdateTrainingType(id, trainingTypeDto, token);
        }
        catch (TrainingTypeNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }
}