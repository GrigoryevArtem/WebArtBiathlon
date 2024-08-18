using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.StatisticalAnalysis.ClusterAnalysis;

public class CentroidsNullReferenceException : NullReferenceException
{
    public CentroidsNullReferenceException() : base(
        "Centroids is undefined. It is necessary to conduct cluster analysis")
    {
    }

    protected CentroidsNullReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CentroidsNullReferenceException(string? message) : base(message)
    {
    }

    public CentroidsNullReferenceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}