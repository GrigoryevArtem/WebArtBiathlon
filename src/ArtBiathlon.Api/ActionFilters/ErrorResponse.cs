using System.Net;

namespace ArtBiathlon.Api.ActionFilters;

public class ErrorResponse
{
    public ErrorResponse(HttpStatusCode statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public HttpStatusCode StatusCode { get; }
    public string Message { get; }
}