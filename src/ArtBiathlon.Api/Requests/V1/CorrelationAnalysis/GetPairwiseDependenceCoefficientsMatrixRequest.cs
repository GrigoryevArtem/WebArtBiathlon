using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Api.Requests.V1.CorrelationAnalysis;

public record GetPairwiseDependenceCoefficientsMatrixRequest(HrvDto[] HrvDtos);