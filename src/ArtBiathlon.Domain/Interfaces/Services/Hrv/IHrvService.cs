using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Domain.Interfaces.Services.Hrv;

public interface IHrvService
{
    Task<long> CreateHrvAsync(HrvDto hrvDto, CancellationToken token);
    Task<ModelDtoWithId<HrvDto>> GetHrvByIdAsync(long id, CancellationToken token);
    Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(CancellationToken token);
    Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(int[] biathleteIds, int[] campIds, CancellationToken token);

    Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(int[] biathleteIds, int[] campIds, DayType dayType,
        CancellationToken token);

    Task DeleteHrvAsync(long id, CancellationToken token);
    Task UpdateHrvAsync(long id, HrvDto hrvDto, CancellationToken token);
}