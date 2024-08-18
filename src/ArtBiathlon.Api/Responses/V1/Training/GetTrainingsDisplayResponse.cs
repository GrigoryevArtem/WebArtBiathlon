using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Training;

namespace ArtBiathlon.Api.Responses.V1.Training;

public record GetTrainingsDisplayResponse(
    ModelDtoWithId<TrainingDisplayDto>[] TrainingsDisplayModel);