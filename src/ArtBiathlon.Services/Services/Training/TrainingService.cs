using System.Data;
using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.Training;
using ArtBiathlon.Domain.Interfaces.Dal.Training;
using ArtBiathlon.Domain.Interfaces.Services.Training;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Training;
using FluentValidation;


namespace ArtBiathlon.Services.Services.Training;

internal class TrainingService : ITrainingService
{
    private readonly ITrainingRepository _trainingRepository;
    private readonly IValidator<TrainingDto> _validator;

    public TrainingService(ITrainingRepository trainingRepository, IValidator<TrainingDto> validator)
    {
        _trainingRepository = trainingRepository;
        _validator = validator;
    }

    public async Task<long> CreateTrainingAsync(TrainingDto trainingDto, CancellationToken token)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(
                trainingDto,
                cancellationToken: token);

            return await _trainingRepository.CreateTrainingAsync(trainingDto, token);
        }
        catch (TrainingAlreadyExistsException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingDto>> GetTrainingByIdAsync(long id, CancellationToken token)
    {
        try
        {
            return await _trainingRepository.GetTrainingByIdAsync(id, token);
        }
        catch (TrainingNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingDto>[]> GetTrainingsAsync(CancellationToken token)
    {
        try
        {
            return await _trainingRepository.GetTrainingsAsync(token);
        }
        catch (TrainingsNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task DeleteTrainingAsync(long id, CancellationToken token)
    {
        try
        {
            await _trainingRepository.DeleteTrainingAsync(id, token);
        }
        catch (TrainingNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task UpdateTrainingAsync(long id, TrainingDto trainingDto, CancellationToken token)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(
                trainingDto,
                cancellationToken: token);

            await _trainingRepository.UpdateTrainingAsync(id, trainingDto, token);
        }
        catch (TrainingNotFoundException ex)
        {
            throw new DataException(ex.Message);
        }
    }
}