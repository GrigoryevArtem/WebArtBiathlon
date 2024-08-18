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
        await _validator.ValidateAndThrowAsync(trainingTypeDto, token);
        return await _trainingTypeRepository.CreateTrainingTypeAsync(trainingTypeDto, token);
    }

    public async Task<ModelDtoWithId<TrainingTypeDto>> GetTrainingTypeByIdAsync(byte id, CancellationToken token)
    {
        return await _trainingTypeRepository.GetTrainingTypeByIdAsync(id, token);
    }

    public async Task<ModelDtoWithId<TrainingTypeDto>[]> GetTrainingTypesAsync(CancellationToken token)
    {
        return await _trainingTypeRepository.GetTrainingTypesAsync(token);
    }

    public async Task DeleteTrainingTypeAsync(byte id, CancellationToken token)
    {
        await _trainingTypeRepository.DeleteTrainingTypeAsync(id, token);
    }

    public async Task UpdateTrainingType(byte id, TrainingTypeDto trainingTypeDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(trainingTypeDto, token);
        await _trainingTypeRepository.UpdateTrainingType(id, trainingTypeDto, token);
    }
}