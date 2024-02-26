using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.TrainingsCamp;

public class TrainingsCampsNotFoundException : Exception
{
    public TrainingsCampsNotFoundException() : base("Trainings camps not found")
    {
    }

    protected TrainingsCampsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingsCampsNotFoundException(string? message) : base(message)
    {
    }

    public TrainingsCampsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}