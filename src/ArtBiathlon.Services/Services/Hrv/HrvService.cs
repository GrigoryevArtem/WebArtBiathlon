using ArtBiathlon.Domain.Exceptions.Hrv;
using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Dal.Hrv;
using ArtBiathlon.Domain.Interfaces.Services.Hrv;
using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Services.Services.Hrv;

public class HrvService : IHrvService
{
    private readonly IHrvRepository _hrvRepository;

    public HrvService(IHrvRepository hrvRepository)
    {
        _hrvRepository = hrvRepository;
    }

    public async Task<long> CreateHrvAsync(HrvDto hrvDto, CancellationToken token)
    {
        try
        {
            return await _hrvRepository.CreateHrvAsync(hrvDto, token);
        }
        catch (HrvIndicatorsAlreadyExistsForThisDateException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<HrvDto> GetHrvByIdAsync(long id, CancellationToken token)
    {
        try
        {
            return await _hrvRepository.GetHrvByIdAsync(id, token);
        }
        catch (HrvIndicatorsNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<HrvDto[]> GetHrvsAsync(CancellationToken token)
    {
        try
        {
            return await _hrvRepository.GetHrvsAsync(token);
        }
        catch (HrvIndicatorsNotFoundInThisTimeException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task DeleteHrvAsync(long id, CancellationToken token)
    {
        try
        {
            await _hrvRepository.DeleteHrvAsync(id, token);
        }
        catch (HrvIndicatorsNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task UpdateHrvAsync(long id, HrvDto hrvDto, CancellationToken token)
    {
        try
        {
            await _hrvRepository.UpdateHrvAsync(id, hrvDto, token);
        }
        catch (HrvIndicatorsNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }
}