using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.Hrv;

public class HrvIndicatorsNotFoundException : Exception
{
    public HrvIndicatorsNotFoundException() : base("Hrv indicators not found")
    {
    }

    protected HrvIndicatorsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public HrvIndicatorsNotFoundException(string? message) : base(message)
    {
    }

    public HrvIndicatorsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}