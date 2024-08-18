namespace ArtBiathlon.Domain.Models.Hrv;

public record HrvDto(
    DateTimeOffset CreatedAt,
    long UserInfoId,
    double Readiness,
    long Heart,
    long Rmssd,
    double Rr,
    double Sdnn,
    double Sd,
    double Tp,
    double Hf,
    double Lf,
    double Si,
    double? Load
);