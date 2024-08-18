using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Exceptions.Hrv;
using ArtBiathlon.Domain.Interfaces.Dal.Hrv;
using ArtBiathlon.Domain.Interfaces.Services.Hrv;

namespace ArtBiathlon.Services.Services.Hrv.HrvsFactory;

public class HrvsDayTypeFactory
{
    private readonly IHrvRepository _hrvRepository;

    public HrvsDayTypeFactory(IHrvRepository hrvRepository)
    {
        _hrvRepository = hrvRepository;
    }

    public IHrvsDayType GetHrvs(DayType dayType)
    {
        return dayType switch
        {
            DayType.Weekend => new WeekendHrvsDayType(_hrvRepository),
            DayType.Weekdays => new WeekdayHrvsDayType(_hrvRepository),
            DayType.Alldays => new AlldayHrvsDayType(_hrvRepository),
            _ => throw new HrvIndicatorsNotFoundInThisTimeException()
        };
    }
}