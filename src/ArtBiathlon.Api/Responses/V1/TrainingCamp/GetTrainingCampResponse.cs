using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingsCamp;

namespace ArtBiathlon.Api.Responses.V1.TrainingCamp;

public record GetTrainingCampResponse(ModelDtoWithId<TrainingsCampDto> TrainingCampModel);