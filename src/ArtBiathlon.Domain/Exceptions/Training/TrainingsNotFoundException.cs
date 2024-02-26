using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.Training;

public class TrainingsNotFoundException : Exception
{
    public TrainingsNotFoundException() : base("Trainings not found")
    {
    }

    protected TrainingsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingsNotFoundException(string? message) : base(message)
    {
    }

    public TrainingsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}