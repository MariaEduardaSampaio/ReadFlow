using Domain.Common.Interfaces;
using Infrastructure.Configurations;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var efConfiguration = configuration.GetRequiredSection(nameof(EfCoreConfiguration)).Get<EfCoreConfiguration>()!;
        var environment = configuration.GetRequiredSection(nameof(EnvironmentConfiguration)).Get<EnvironmentConfiguration>()!;

        services.AddDbContext<IUnitOfWork, ReadFlowDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("Postgres"),
                npgsql =>
                {
                    npgsql.MigrationsAssembly(typeof(ReadFlowDbContext).Assembly.FullName);
                });
            
            if (efConfiguration.EnableLog)
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole(x =>
                {
                    x.FormatterName = environment.FormatterName;
                })));
            }

            if (efConfiguration.EnableSensitiveDataLogging)
            {
                options.EnableSensitiveDataLogging();
            }
        });

        return services;
    }
}