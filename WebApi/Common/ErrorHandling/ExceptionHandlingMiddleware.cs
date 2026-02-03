using WebApi.Common.ErrorHandling.Interfaces;

namespace WebApi.Common.ErrorHandling;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, 
    IExceptionToProblemDetailsMapper mapper)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var problem = mapper.Map(ex, context);

            problem.Instance ??= context.Request.Path;
            problem.Extensions["traceId"] = context.TraceIdentifier;

            var status = problem.Status ?? StatusCodes.Status500InternalServerError;
            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";

            if (status >= 500)
            {
                logger.LogError(ex, "Unhandled exception");
            }
            else
            {
                logger.LogWarning(ex, "Request failed with status {Status}", status);
            }

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}