using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.User;

public class EmailNotConfirmedException : Exception
{
    public EmailNotConfirmedException() : base("Email not confirmed")
    {
    }

    protected EmailNotConfirmedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public EmailNotConfirmedException(string? message) : base(message)
    {
    }

    public EmailNotConfirmedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}