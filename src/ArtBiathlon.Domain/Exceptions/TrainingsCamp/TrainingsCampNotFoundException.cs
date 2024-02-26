using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.TrainingsCamp;

public class TrainingsCampNotFoundException : Exception
{
    public TrainingsCampNotFoundException() : base("Trainings camp not found")
    {
    }

    protected TrainingsCampNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingsCampNotFoundException(string? message) : base(message)
    {
    }

    public TrainingsCampNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}