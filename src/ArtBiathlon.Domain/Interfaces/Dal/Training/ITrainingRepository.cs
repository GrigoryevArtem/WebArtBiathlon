using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Training;

namespace ArtBiathlon.Domain.Interfaces.Dal.Training;

public interface ITrainingRepository
{
    Task<long> CreateTrainingAsync(TrainingDto trainingDto, CancellationToken token);
    Task<ModelDtoWithId<TrainingDto>> GetTrainingByIdAsync(long id, CancellationToken token);
    Task<ModelDtoWithId<TrainingDto>[]> GetTrainingsAsync(CancellationToken token);
    Task<ModelDtoWithId<TrainingDisplayDto>[]> GetTrainingsDisplayAsync(CancellationToken token);
    Task DeleteTrainingAsync(long id, CancellationToken token);
    Task UpdateTrainingAsync(long id, TrainingDto trainingDto, CancellationToken token);
}