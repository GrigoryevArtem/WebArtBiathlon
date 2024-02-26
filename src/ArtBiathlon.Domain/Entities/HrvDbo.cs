namespace ArtBiathlon.Domain.Entities;

public record HrvDbo
{
    public DateTimeOffset Date { get; init; }
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
}