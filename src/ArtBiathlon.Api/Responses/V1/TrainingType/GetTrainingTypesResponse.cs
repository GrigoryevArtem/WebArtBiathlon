using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingType;

namespace ArtBiathlon.Api.Responses.V1.TrainingType;

public record GetTrainingTypesResponse(ModelDtoWithId<TrainingTypeDto>[] TrainingTypeModel);