using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.TrainingsCamp;
using ArtBiathlon.Domain.Exceptions.TrainingSchedule;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Dal.TrainingsSchedule;
using ArtBiathlon.Domain.Interfaces.Services.TrainingsSchedule;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;

namespace ArtBiathlon.Services.Services.TrainingsSchedule;

internal class TrainingsScheduleService : ITrainingsScheduleService
{
    private readonly ITrainingsScheduleRepository _trainingsScheduleRepository;

    public TrainingsScheduleService(ITrainingsScheduleRepository trainingsScheduleRepository)
    {
        _trainingsScheduleRepository = trainingsScheduleRepository;
    }

    public async Task<long> CreateTrainingsScheduleAsync(TrainingsScheduleDto trainingScheduleDto,
        CancellationToken token)
    {
        try
        {
            return await _trainingsScheduleRepository.CreateTrainingsScheduleAsync(trainingScheduleDto, token);
        }
        catch (TrainingScheduleAlreadyExistsException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleByIdAsync(long id,
        CancellationToken token)
    {
        try
        {
            return await _trainingsScheduleRepository.GetTrainingsScheduleByIdAsync(id, token);
        }
        catch (TrainingScheduleNotFoundException ex)
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

    public async Task UpdateTrainingScheduleAsync(long id, TrainingsScheduleDto trainingsScheduleDto, CancellationToken token)
    {
        try
        {
            await _trainingsScheduleRepository.UpdateTrainingScheduleAsync(id, trainingsScheduleDto, token);
        }
        catch (TrainingScheduleNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }
}