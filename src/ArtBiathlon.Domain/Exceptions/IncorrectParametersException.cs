using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions;

public class IncorrectParametersException : Exception
{
    public IncorrectParametersException()
    {
    }

    protected IncorrectParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public IncorrectParametersException(string? message) : base(message)
    {
    }

    public IncorrectParametersException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}