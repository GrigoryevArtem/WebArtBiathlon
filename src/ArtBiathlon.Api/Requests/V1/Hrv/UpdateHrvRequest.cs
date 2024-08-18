using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Api.Requests.V1.Hrv;

public record UpdateHrvRequest(
    long Id,
    HrvDto HrvModel);