using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common.ErrorHandling.Interfaces;

public interface IProblemDetailsExceptionMapper
{
    bool CanHandle(Exception exception);
    ProblemDetails Map(Exception exception, HttpContext httpContext);
}