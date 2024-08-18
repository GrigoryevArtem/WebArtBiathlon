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
        await _validator.ValidateAndThrowAsync(trainingDto, token);
        return await _trainingRepository.CreateTrainingAsync(trainingDto, token);
    }

    public async Task<ModelDtoWithId<TrainingDto>> GetTrainingByIdAsync(long id, CancellationToken token)
    {
        return await _trainingRepository.GetTrainingByIdAsync(id, token);
    }

    public async Task<ModelDtoWithId<TrainingDto>[]> GetTrainingsAsync(CancellationToken token)
    {
        return await _trainingRepository.GetTrainingsAsync(token);
    }

    public async Task<ModelDtoWithId<TrainingDisplayDto>[]> GetTrainingsDisplayAsync(CancellationToken token)
    {
        return await _trainingRepository.GetTrainingsDisplayAsync(token);
    }

    public async Task DeleteTrainingAsync(long id, CancellationToken token)
    {
        await _trainingRepository.DeleteTrainingAsync(id, token);
    }

    public async Task UpdateTrainingAsync(long id, TrainingDto trainingDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(
            trainingDto,
            token);

        await _trainingRepository.UpdateTrainingAsync(id, trainingDto, token);
    }
}