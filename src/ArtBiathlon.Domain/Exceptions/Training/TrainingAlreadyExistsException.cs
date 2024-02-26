using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.Training;

public class TrainingAlreadyExistsException : Exception
{
    public TrainingAlreadyExistsException() : base("This training already exists")
    {
    }

    protected TrainingAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingAlreadyExistsException(string? message) : base(message)
    {
    }

    public TrainingAlreadyExistsException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}