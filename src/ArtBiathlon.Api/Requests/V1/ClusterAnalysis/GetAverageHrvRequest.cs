using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Api.Requests.V1.ClusterAnalysis;

public record GetAverageHrvRequest(HrvDto[] HrvDtos);