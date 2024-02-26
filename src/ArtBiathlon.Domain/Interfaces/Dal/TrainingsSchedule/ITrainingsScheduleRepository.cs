using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;

namespace ArtBiathlon.Domain.Interfaces.Dal.TrainingsSchedule;

public interface ITrainingsScheduleRepository
{
    Task<long> CreateTrainingsScheduleAsync(TrainingsScheduleDto trainingScheduleDto,  CancellationToken token);
    Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleByIdAsync(long id,  CancellationToken token);
    Task<ModelDtoWithId<TrainingsScheduleDto>[]> GetTrainingsSchedulesAsync(CancellationToken token);
    Task DeleteTrainingsScheduleAsync(long id, CancellationToken token);
    Task UpdateTrainingScheduleAsync(long id, TrainingsScheduleDto trainingsScheduleDto, CancellationToken token);
}