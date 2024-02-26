using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.Hrv;

public class HrvIndicatorsAlreadyExistsForThisDateException : Exception
{
    public HrvIndicatorsAlreadyExistsForThisDateException() : base("Hrv indicators already exists for this date")
    {
    }

    protected HrvIndicatorsAlreadyExistsForThisDateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public HrvIndicatorsAlreadyExistsForThisDateException(string? message) : base(message)
    {
    }

    public HrvIndicatorsAlreadyExistsForThisDateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}