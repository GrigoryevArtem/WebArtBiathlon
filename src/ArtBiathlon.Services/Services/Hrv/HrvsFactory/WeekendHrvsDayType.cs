using ArtBiathlon.Domain.Interfaces.Dal.Hrv;
using ArtBiathlon.Domain.Interfaces.Services.Hrv;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Services.Services.Hrv.HrvsFactory;

public class WeekendHrvsDayType : IHrvsDayType
{
    private readonly IHrvRepository _hrvRepository;

    public WeekendHrvsDayType(IHrvRepository hrvRepository)
    {
        _hrvRepository = hrvRepository;
    }

    public async Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(int[] biathleteIds, int[] campIds, CancellationToken token)
    {
        return await _hrvRepository.GetHrvsWeekendAsync(biathleteIds, campIds, token);
    }
}