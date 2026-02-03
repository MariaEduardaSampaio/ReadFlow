using Microsoft.AspNetCore.Mvc;
using WebApi.Common.ErrorHandling.Interfaces;

namespace WebApi.Common.ErrorHandling;

public sealed class CompositeExceptionToProblemDetailsMapper(IEnumerable<IProblemDetailsExceptionMapper> mappers)
    : IExceptionToProblemDetailsMapper
{
    private readonly IReadOnlyCollection<IProblemDetailsExceptionMapper> _mappers = mappers.ToArray();

    public ProblemDetails Map(Exception exception, HttpContext httpContext)
    {
        var mapper = _mappers.FirstOrDefault(m => m.CanHandle(exception));

        if (mapper != null)
        {
            return mapper.Map(exception, httpContext);
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