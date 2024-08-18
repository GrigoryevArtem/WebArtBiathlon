using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Api.Requests.V1.Hrv;

public record HrvFilterByBiathleteIdAndCampIdRequest(
    int[] UserInfoId,
    int[] CampId,
    DayType DayType);