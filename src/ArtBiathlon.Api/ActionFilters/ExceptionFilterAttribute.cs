using System.Net;
using ArtBiathlon.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ArtBiathlon.Api.ActionFilters;

public sealed class ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var httpStatusCode = context.Exception switch
        {
            ValidationException => HttpStatusCode.BadRequest,
            NotFoundException => HttpStatusCode.NotFound,
            IncorrectParametersException => HttpStatusCode.BadRequest,
            AlreadyExistException => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };

        var jsonResult = new JsonResult(
            new ErrorResponse(
                httpStatusCode,
                context.Exception.Message))
        {
            StatusCode = (int)httpStatusCode
        };

        context.Result = jsonResult;
    }
}