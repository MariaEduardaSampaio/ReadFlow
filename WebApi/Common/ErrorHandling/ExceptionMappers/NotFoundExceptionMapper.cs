using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.ErrorHandling.Interfaces;

namespace WebApi.Common.ErrorHandling.ExceptionMappers;

public class NotFoundExceptionMapper: IProblemDetailsExceptionMapper
{
    public bool CanHandle(Exception exception) => exception is NotFoundException;


    public ProblemDetails Map(Exception exception, HttpContext httpContext)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Entity Not Found",
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };
    }
}