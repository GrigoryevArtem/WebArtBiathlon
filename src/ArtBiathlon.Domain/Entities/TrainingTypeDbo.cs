using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingType;

namespace ArtBiathlon.Domain.Entities;

public record TrainingTypeDbo
{
    public TrainingTypeDbo()
    {
    }

    public TrainingTypeDbo(byte id, string typeName)
    {
        Id = id;
        TypeName = typeName;
    }

    public byte Id { get; init; }
    public string TypeName { get; init; }

    public ModelDtoWithId<TrainingTypeDto> ToModelWithId()
    {
        var trainingTypeModel = new TrainingTypeDto(
            TypeName);

        return new ModelDtoWithId<TrainingTypeDto>(
            Id,
            trainingTypeModel);
    }
}