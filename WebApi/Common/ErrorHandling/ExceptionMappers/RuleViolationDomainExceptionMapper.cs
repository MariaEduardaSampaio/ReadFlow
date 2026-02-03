using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.ErrorHandling.Interfaces;

namespace WebApi.Common.ErrorHandling.ExceptionMappers;

public sealed class RuleViolationDomainExceptionMapper : IProblemDetailsExceptionMapper
{
    public bool CanHandle(Exception exception) => exception is RuleViolationDomainException;

    public ProblemDetails Map(Exception exception, HttpContext httpContext)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation error",
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };
    }
}