using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.TrainingSchedule;
using ArtBiathlon.Domain.Interfaces.Dal.TrainingsSchedule;
using ArtBiathlon.Domain.Interfaces.Services.TrainingsSchedule;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;
using FluentValidation;

namespace ArtBiathlon.Services.Services.TrainingsSchedule;

internal class TrainingsScheduleService : ITrainingsScheduleService
{
    private readonly ITrainingsScheduleRepository _trainingsScheduleRepository;
    private readonly IValidator<TrainingsScheduleDto> _validator;

    public TrainingsScheduleService(ITrainingsScheduleRepository trainingsScheduleRepository,
        IValidator<TrainingsScheduleDto> validator)
    {
        _trainingsScheduleRepository = trainingsScheduleRepository;
        _validator = validator;
    }

    public async Task<long> CreateTrainingsScheduleAsync(TrainingsScheduleDto trainingsScheduleDto,
        CancellationToken token)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(
                trainingsScheduleDto,
                cancellationToken: token);

            return await _trainingsScheduleRepository.CreateTrainingsScheduleAsync(trainingsScheduleDto, token);
        }
        catch (TrainingScheduleAlreadyExistsException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleAsync(long id,
        CancellationToken token)
    {
        try
        {
            return await _trainingsScheduleRepository.GetTrainingsScheduleAsync(id, token);
        }
        catch (TrainingScheduleNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleAsync(DateTimeOffset startDate, CancellationToken token)
    {
        try
        {
            return await _trainingsScheduleRepository.GetTrainingsScheduleAsync(startDate, token);
        }
        catch (TrainingSchedulesNotFoundForThisDateException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>[]> GetTrainingsSchedulesAsync(CancellationToken token)
    {
        try
        {
            return await _trainingsScheduleRepository.GetTrainingsSchedulesAsync(token);
        }
        catch (Exception ex) //todo: заглушка, чекни репу
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task DeleteTrainingsScheduleAsync(long id, CancellationToken token)
    {
        try
        {
            await _trainingsScheduleRepository.DeleteTrainingsScheduleAsync(id, token);
        }
        catch (TrainingScheduleNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task UpdateTrainingScheduleAsync(long id, TrainingsScheduleDto trainingsScheduleDto,
        CancellationToken token)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(
                trainingsScheduleDto,
                cancellationToken: token);

            await _trainingsScheduleRepository.UpdateTrainingScheduleAsync(id, trainingsScheduleDto, token);
        }
        catch (TrainingScheduleNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }
}