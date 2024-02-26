using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.TrainingType;

public class TrainingTypeNotFoundException : Exception
{
    public TrainingTypeNotFoundException() : base("Training type not found")
    {
    }

    protected TrainingTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingTypeNotFoundException(string? message) : base(message)
    {
    }

    public TrainingTypeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}