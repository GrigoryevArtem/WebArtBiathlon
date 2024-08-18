using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Domain.Entities;

public record HrvDbo
{
    public HrvDbo()
    {
    }

    public HrvDbo
    (
        long id,
        DateTimeOffset createdAt,
        long userInfoId,
        double readiness,
        long heart,
        long rmssd,
        double rr,
        double sdnn,
        double sd,
        double tp,
        double hf,
        double lf,
        double si,
        double? load)
    {
        Id = id;
        CreatedAt = createdAt;
        UserInfoId = userInfoId;
        Readiness = readiness;
        Heart = heart;
        Rmssd = rmssd;
        Rr = rr;
        Sdnn = sdnn;
        Sd = sd;
        Tp = tp;
        Hf = hf;
        Lf = lf;
        Si = si;
        Load = load;
    }

    public long Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }

    public long UserInfoId { get; init; }
    public double Readiness { get; init; }
    public long Heart { get; init; }
    public long Rmssd { get; init; }
    public double Rr { get; init; }
    public double Sdnn { get; init; }
    public double Sd { get; init; }
    public double Tp { get; init; }
    public double Hf { get; init; }
    public double Lf { get; init; }
    public double Si { get; init; }
    public double? Load { get; init; }

    public ModelDtoWithId<HrvDto> ToModelWithId()
    {
        var hrvModel = new HrvDto(
            CreatedAt,
            UserInfoId,
            Readiness,
            Heart,
            Rmssd,
            Rr,
            Sdnn,
            Sd,
            Tp,
            Hf,
            Lf,
            Si,
            Load
        );

        return new ModelDtoWithId<HrvDto>(
            Id,
            hrvModel);
    }
}