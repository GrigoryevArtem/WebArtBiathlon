using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Training;

namespace ArtBiathlon.Domain.Entities;

public record TrainingDbo
{
    public long Id { get; init; }
    public string TrainingName { get; init; }
    public byte TrainingTypeId { get; init; }
    
    public TrainingDbo()
    {
    }

    public TrainingDbo(long id, string trainingName, byte trainingTypeId)
    {
        Id = id;
        TrainingName = trainingName;
        TrainingTypeId = trainingTypeId;
    }

    public ModelDtoWithId<TrainingDto> ToModelWithId()
    {
        var trainingModel = new TrainingDto(
            TrainingName,
            TrainingTypeId
        );

        return new ModelDtoWithId<TrainingDto>(
            Id,
            trainingModel);
    }
}