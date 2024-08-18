using ArtBiathlon.Domain.Interfaces.Dal.TrainingCamp;
using ArtBiathlon.Domain.Interfaces.Services.TrainingCamp;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingsCamp;
using FluentValidation;

namespace ArtBiathlon.Services.Services.TrainingCamp;

internal class TrainingCampService : ITrainingCampService
{
    private readonly ITrainingCampRepository _trainingCampRepository;
    private readonly IValidator<TrainingsCampDto> _validator;

    public TrainingCampService(ITrainingCampRepository trainingCampRepository, IValidator<TrainingsCampDto> validator)
    {
        _trainingCampRepository = trainingCampRepository;
        _validator = validator;
    }

    public async Task<long> CreateTrainingCampAsync(TrainingsCampDto trainingsCampDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(trainingsCampDto, token);
        return await _trainingCampRepository.CreateTrainingCampAsync(trainingsCampDto, token);
    }

    public async Task<ModelDtoWithId<TrainingsCampDto>> GetTrainingCampByIdAsync(long id, CancellationToken token)
    {
        return await _trainingCampRepository.GetTrainingCampByIdAsync(id, token);
    }

    public async Task<ModelDtoWithId<TrainingsCampDto>[]> GetTrainingCampsAsync(CancellationToken token)
    {
        return await _trainingCampRepository.GetTrainingCampsAsync(token);
    }

    public async Task DeleteTrainingCampAsync(long id, CancellationToken token)
    {
        await _trainingCampRepository.DeleteTrainingCampAsync(id, token);
    }

    public async Task UpdateTrainingCampAsync(long id, TrainingsCampDto trainingsCampDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(trainingsCampDto, token);
        await _trainingCampRepository.UpdateTrainingCampAsync(id, trainingsCampDto, token);
    }
}