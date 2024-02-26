using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.UserInfo;

public class UsersInfoNotFoundException : Exception
{
    public UsersInfoNotFoundException() : base("Users info not found")
    {
    }

    protected UsersInfoNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UsersInfoNotFoundException(string? message) : base(message)
    {
    }

    public UsersInfoNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}