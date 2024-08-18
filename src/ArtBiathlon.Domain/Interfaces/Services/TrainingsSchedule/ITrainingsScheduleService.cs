using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;

namespace ArtBiathlon.Domain.Interfaces.Services.TrainingsSchedule;

public interface ITrainingsScheduleService
{
    Task<long> CreateTrainingsScheduleAsync(TrainingsScheduleDto trainingScheduleDto, CancellationToken token);
    Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleAsync(long id, CancellationToken token);

    Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleAsync(DateTimeOffset startDate,
        CancellationToken token);

    Task<ModelDtoWithId<TrainingsScheduleDisplayDto>[]> GetTrainingsSchedulesDisplayAsync(CancellationToken token);
    Task<ModelDtoWithId<TrainingsScheduleDto>[]> GetTrainingsSchedulesAsync(CancellationToken token);
    Task DeleteTrainingsScheduleAsync(long id, CancellationToken token);
    Task UpdateTrainingScheduleAsync(long id, TrainingsScheduleDto trainingsScheduleDto, CancellationToken token);
}