using ArtBiathlon.Domain.Models.TrainingType;

namespace ArtBiathlon.Api.Requests.V1.TrainingType;

public record UpdateTrainingTypeRequest(
    byte Id,
    TrainingTypeDto TrainingTypeModel);