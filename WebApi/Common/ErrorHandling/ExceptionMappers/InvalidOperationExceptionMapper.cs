using Microsoft.AspNetCore.Mvc;
using WebApi.Common.ErrorHandling.Interfaces;

namespace WebApi.Common.ErrorHandling.ExceptionMappers;

public sealed class InvalidOperationExceptionMapper : IProblemDetailsExceptionMapper
{
    public bool CanHandle(Exception exception) => exception is InvalidOperationException;

    public ProblemDetails Map(Exception exception, HttpContext httpContext)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Invalid operation",
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };
    }
}