using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common.ErrorHandling;

public sealed class ExceptionToProblemDetailsMapper : IExceptionToProblemDetailsMapper
{
    public ProblemDetails Map(Exception exception, HttpContext httpContext)
    {
        if (exception is RuleViolationDomainException)
        {
            return new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation error",
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };
        }

        return new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = "An unexpected error occurred.",
            Instance = httpContext.Request.Path
        };
    }
}