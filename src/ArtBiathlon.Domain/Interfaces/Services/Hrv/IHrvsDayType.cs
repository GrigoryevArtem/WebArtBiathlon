using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Domain.Interfaces.Services.Hrv;

public interface IHrvsDayType
{
    Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(int[] biathleteIds, int[] campIds, CancellationToken token);
}