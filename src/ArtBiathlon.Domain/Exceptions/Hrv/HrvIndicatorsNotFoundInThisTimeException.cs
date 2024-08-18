using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.Hrv;

public class HrvIndicatorsNotFoundInThisTimeException : NotFoundException
{
    public HrvIndicatorsNotFoundInThisTimeException() : base("Hrv indicators not found in this time")
    {
    }

    protected HrvIndicatorsNotFoundInThisTimeException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public HrvIndicatorsNotFoundInThisTimeException(string? message) : base(message)
    {
    }

    public HrvIndicatorsNotFoundInThisTimeException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}