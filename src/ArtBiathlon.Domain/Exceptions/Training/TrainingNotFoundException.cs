using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.Training;

public class TrainingNotFoundException : Exception
{
    public TrainingNotFoundException() : base("Training not found")
    {
    }

    protected TrainingNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingNotFoundException(string? message) : base(message)
    {
    }

    public TrainingNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}