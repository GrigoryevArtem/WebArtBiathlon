using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.StatisticalAnalysis.FactorAnalysis;

public class FactorTransformationMatrixNullReferenceException : NullReferenceException
{
    public FactorTransformationMatrixNullReferenceException() : base(
        "Factor transformation matrix is undefined. It is necessary to conduct factor analysis")
    {
    }

    protected FactorTransformationMatrixNullReferenceException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }

    public FactorTransformationMatrixNullReferenceException(string? message) : base(message)
    {
    }

    public FactorTransformationMatrixNullReferenceException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}