using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common.ErrorHandling.Interfaces;

public interface IExceptionToProblemDetailsMapper
{
    ProblemDetails Map(Exception exception, HttpContext httpContext);
}