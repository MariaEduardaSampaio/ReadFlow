using Application.Abstractions.Persistence;
using Infrastructure.Configurations;
using Infrastructure.EntityFramework.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructureConfigurations(IConfiguration configuration)
        {
            services.AddDbContextConfigurations(configuration);
            services.AddRepositories();
        }

        public void AddRepositories()
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private IServiceCollection AddDbContextConfigurations(IConfiguration configuration)
        {
            var efConfiguration = configuration.GetRequiredSection(nameof(EfCoreConfiguration)).Get<EfCoreConfiguration>()!;
            var environment = configuration.GetRequiredSection(nameof(EnvironmentConfiguration)).Get<EnvironmentConfiguration>()!;
        
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
}