using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.TrainingsCamp;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Dal.TrainingCamp;
using ArtBiathlon.Domain.Interfaces.Services.TrainingCamp;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingsCamp;

namespace ArtBiathlon.Services.Services.TrainingCamp;

internal class TrainingCampService : ITrainingCampService
{
    private readonly ITrainingCampRepository _trainingCampRepository;

    public TrainingCampService(ITrainingCampRepository trainingCampRepository)
    {
        _trainingCampRepository = trainingCampRepository;
    }

    public async Task<long> CreateTrainingCampAsync(TrainingsCampDto trainingCampDto, CancellationToken token)
    {
        try
        {
            return await _trainingCampRepository.CreateTrainingCampAsync(trainingCampDto, token);
        }
        catch (Exception
               ex) //todo: заглушка, тут должно быть исключение по типу "В данный интервал времени уже запланирован тренировочный лагерь"
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingsCampDto>> GetTrainingCampByIdAsync(long id, CancellationToken token)
    {
        try
        {
            return await _trainingCampRepository.GetTrainingCampByIdAsync(id, token);
        }
        catch (TrainingsCampNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<TrainingsCampDto>[]> GetTrainingCampsAsync(CancellationToken token)
    {
        try
        {
            return await _trainingCampRepository.GetTrainingCampsAsync(token);
        }
        catch (TrainingsCampsNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task DeleteTrainingCampAsync(long id, CancellationToken token)
    {
        try
        {
            await _trainingCampRepository.DeleteTrainingCampAsync(id, token);
        }
        catch (TrainingsCampNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task UpdateTrainingCampAsync(long id, TrainingsCampDto trainingsCampDto, CancellationToken token)
    {
        try
        {
            await _trainingCampRepository.UpdateTrainingCampAsync(id, trainingsCampDto, token);
        }
        catch (TrainingsCampNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }
}