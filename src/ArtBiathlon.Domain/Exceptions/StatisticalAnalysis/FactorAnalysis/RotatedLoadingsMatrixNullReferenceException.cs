using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.StatisticalAnalysis.FactorAnalysis;

public class RotatedLoadingsMatrixNullReferenceException : NullReferenceException
{
    public RotatedLoadingsMatrixNullReferenceException() : base(
        "Rotated loadings matrix explained is undefined. It is necessary to conduct factor analysis")
    {
    }

    protected RotatedLoadingsMatrixNullReferenceException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }

    public RotatedLoadingsMatrixNullReferenceException(string? message) : base(message)
    {
    }

    public RotatedLoadingsMatrixNullReferenceException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}