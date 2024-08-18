namespace ArtBiathlon.Domain.Models.Training;

public record TrainingDisplayModel
{
    public TrainingDisplayModel()
    {
    }

    public TrainingDisplayModel(long id, string trainingName, string typeName)
    {
        Id = id;
        TrainingName = trainingName;
        TypeName = typeName;
    }

    public long Id { get; init; }
    public string TrainingName { get; init; }
    public string TypeName { get; init; }

    public ModelDtoWithId<TrainingDisplayDto> ToModelWithId()
    {
        var trainingDisplayModel = new TrainingDisplayDto(
            TrainingName,
            TypeName);

        return new ModelDtoWithId<TrainingDisplayDto>(
            Id,
            trainingDisplayModel);
    }
}