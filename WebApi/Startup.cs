using Application;
using Infrastructure;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Seeds;
using WebApi.Common.ErrorHandling;
using WebApi.Common.ErrorHandling.Interfaces;

namespace WebApi;

public sealed class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.RegisterErrorHandlingServices();
        services.RegisterInfrastructureConfigurations(configuration);
        services.RegisterApplicationUseCases();
    }

    public async Task Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(_ => { });
            app.UseSwaggerUI();

            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ReadFlowDbContext>();
            
            await DatabaseSeeder.SeedAsync(db);
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        
        app.MapControllers();
    }
}