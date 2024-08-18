using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.StatisticalAnalysis.FactorAnalysis;

public class EigenValuesNullReferenceException : NullReferenceException
{
    public EigenValuesNullReferenceException() : base(
        "Eigen values is undefined. It is necessary to conduct factor analysis")
    {
    }

    protected EigenValuesNullReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public EigenValuesNullReferenceException(string? message) : base(message)
    {
    }

    public EigenValuesNullReferenceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}