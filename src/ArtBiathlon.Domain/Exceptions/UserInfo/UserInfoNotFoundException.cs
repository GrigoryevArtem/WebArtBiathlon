using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.UserInfo;

public class UserInfoNotFoundException : Exception
{
    public UserInfoNotFoundException() : base("User info not found")
    {
    }

    protected UserInfoNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserInfoNotFoundException(string? message) : base(message)
    {
    }

    public UserInfoNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}