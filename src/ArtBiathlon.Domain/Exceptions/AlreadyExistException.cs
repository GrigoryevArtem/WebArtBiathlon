using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException()
    {
    }

    protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public AlreadyExistException(string? message) : base(message)
    {
    }

    public AlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}