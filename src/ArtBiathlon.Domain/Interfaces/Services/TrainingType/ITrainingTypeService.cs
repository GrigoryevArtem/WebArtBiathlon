using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingType;

namespace ArtBiathlon.Domain.Interfaces.Services.TrainingType;

public interface ITrainingTypeService
{
    Task<byte> CreateTrainingTypeAsync(TrainingTypeDto trainingTypeDto,  CancellationToken token);
    Task<ModelDtoWithId<TrainingTypeDto>> GetTrainingTypeByIdAsync(byte id,  CancellationToken token);
    Task<ModelDtoWithId<TrainingTypeDto>[]> GetTrainingTypesAsync(CancellationToken token);
    Task DeleteTrainingTypeAsync(byte id, CancellationToken token);
    Task UpdateTrainingType(byte id, TrainingTypeDto trainingTypeDto, CancellationToken token);
}