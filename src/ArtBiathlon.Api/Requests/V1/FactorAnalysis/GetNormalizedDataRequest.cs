using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Api.Requests.V1.FactorAnalysis;

public record GetNormalizedDataRequest(HrvDto[] HrvDtos);