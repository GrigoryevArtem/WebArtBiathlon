using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.TrainingType;

public class TrainingTypesNotFoundException : Exception
{
    public TrainingTypesNotFoundException() : base("Training types not found")
    {
    }

    protected TrainingTypesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingTypesNotFoundException(string? message) : base(message)
    {
    }

    public TrainingTypesNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}