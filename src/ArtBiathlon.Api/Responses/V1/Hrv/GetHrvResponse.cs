using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Api.Responses.V1.Hrv;

public record GetHrvResponse(ModelDtoWithId<HrvDto> HrvModel);