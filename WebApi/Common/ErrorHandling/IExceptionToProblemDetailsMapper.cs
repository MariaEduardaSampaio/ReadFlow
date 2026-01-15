using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common.ErrorHandling;

public interface IExceptionToProblemDetailsMapper
{
    ProblemDetails Map(Exception exception, HttpContext httpContext);
}