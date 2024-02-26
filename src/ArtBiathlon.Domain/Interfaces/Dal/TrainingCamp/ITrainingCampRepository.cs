using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingsCamp;

namespace ArtBiathlon.Domain.Interfaces.Dal.TrainingCamp;

public interface ITrainingCampRepository
{
    Task<long> CreateTrainingCampAsync(TrainingsCampDto trainingCampDto,  CancellationToken token);
    Task<ModelDtoWithId<TrainingsCampDto>> GetTrainingCampByIdAsync(long id,  CancellationToken token);
    Task<ModelDtoWithId<TrainingsCampDto>[]> GetTrainingCampsAsync(CancellationToken token);
    Task DeleteTrainingCampAsync(long id, CancellationToken token);
    Task UpdateTrainingCampAsync(long id, TrainingsCampDto trainingsCampDto, CancellationToken token);
}