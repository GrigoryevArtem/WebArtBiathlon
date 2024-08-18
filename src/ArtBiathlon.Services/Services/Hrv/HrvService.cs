using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Interfaces.Dal.Hrv;
using ArtBiathlon.Domain.Interfaces.Services.Hrv;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Services.Services.Hrv.HrvsFactory;
using FluentValidation;

namespace ArtBiathlon.Services.Services.Hrv;

public class HrvService : IHrvService
{
    private readonly HrvsDayTypeFactory _hrvDayTypeFactory;
    private readonly IHrvRepository _hrvRepository;
    private readonly IValidator<HrvDto> _validator;

    public HrvService(IHrvRepository hrvRepository, IValidator<HrvDto> validator, HrvsDayTypeFactory hrvDayTypeFactory)
    {
        _hrvRepository = hrvRepository;
        _validator = validator;
        _hrvDayTypeFactory = hrvDayTypeFactory;
    }

    public async Task<long> CreateHrvAsync(HrvDto hrvDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(hrvDto, token);
        return await _hrvRepository.CreateHrvAsync(hrvDto, token);
    }

    public async Task<ModelDtoWithId<HrvDto>> GetHrvByIdAsync(long id, CancellationToken token)
    {
        return await _hrvRepository.GetHrvByIdAsync(id, token);
    }

    public async Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(CancellationToken token)
    {
        return await _hrvRepository.GetHrvsAsync(token);
    }

    public async Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(int[] biathleteIds, int[] campIds, CancellationToken token)
    {
        return await _hrvRepository.GetHrvsAsync(biathleteIds, campIds, token);
    }

    public async Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(int[] biathleteIds, int[] campIds, DayType dayType,
        CancellationToken token)
    {
        var strategy = _hrvDayTypeFactory.GetHrvs(dayType);
        return await strategy.GetHrvsAsync(biathleteIds, campIds, token);
    }


    public async Task DeleteHrvAsync(long id, CancellationToken token)
    {
        await _hrvRepository.DeleteHrvAsync(id, token);
    }

    public async Task UpdateHrvAsync(long id, HrvDto hrvDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(hrvDto, token);
        await _hrvRepository.UpdateHrvAsync(id, hrvDto, token);
    }
}