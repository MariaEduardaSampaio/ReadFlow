using WebApi.Common.ErrorHandling;
using WebApi.Common.ErrorHandling.ExceptionMappers;
using WebApi.Common.ErrorHandling.Interfaces;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection RegisterErrorHandlingServices(this IServiceCollection services)
    {
        services.AddSingleton<IProblemDetailsExceptionMapper, RuleViolationDomainExceptionMapper>();
        services.AddSingleton<IProblemDetailsExceptionMapper, InvalidOperationExceptionMapper>();
        services.AddSingleton<IProblemDetailsExceptionMapper, NotFoundExceptionMapper>();

        services.AddSingleton<IExceptionToProblemDetailsMapper, CompositeExceptionToProblemDetailsMapper>();

        return services;
    }
}