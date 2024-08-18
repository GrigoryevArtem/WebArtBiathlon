using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.User;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException() : base("User not found")
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}