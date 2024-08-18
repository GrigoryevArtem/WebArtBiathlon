using ArtBiathlon.Domain.Models.Training;

namespace ArtBiathlon.Api.Requests.V1.Training;

public record AddTrainingRequest(TrainingDto TrainingModel);