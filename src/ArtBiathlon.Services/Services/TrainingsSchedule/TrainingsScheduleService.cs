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
        await _validator.ValidateAndThrowAsync(trainingsScheduleDto, token);
        return await _trainingsScheduleRepository.CreateTrainingsScheduleAsync(trainingsScheduleDto, token);
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleAsync(long id,
        CancellationToken token)
    {
        return await _trainingsScheduleRepository.GetTrainingsScheduleAsync(id, token);
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleAsync(DateTimeOffset startDate,
        CancellationToken token)
    {
        return await _trainingsScheduleRepository.GetTrainingsScheduleAsync(startDate, token);
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDisplayDto>[]> GetTrainingsSchedulesDisplayAsync(
        CancellationToken token)
    {
        return await _trainingsScheduleRepository.GetTrainingsSchedulesDisplayAsync(token);
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>[]> GetTrainingsSchedulesAsync(CancellationToken token)
    {
        return await _trainingsScheduleRepository.GetTrainingsSchedulesAsync(token);
    }

    public async Task DeleteTrainingsScheduleAsync(long id, CancellationToken token)
    {
        await _trainingsScheduleRepository.DeleteTrainingsScheduleAsync(id, token);
    }

    public async Task UpdateTrainingScheduleAsync(long id, TrainingsScheduleDto trainingsScheduleDto,
        CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(trainingsScheduleDto, token);
        await _trainingsScheduleRepository.UpdateTrainingScheduleAsync(id, trainingsScheduleDto, token);
    }
}