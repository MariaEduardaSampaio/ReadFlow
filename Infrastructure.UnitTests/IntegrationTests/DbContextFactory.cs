using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitTests.IntegrationTests;

public static class DbContextFactory
{
    public static ReadFlowDbContext Create(string connectionString)
    {
        var options = new DbContextOptionsBuilder<ReadFlowDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new ReadFlowDbContext(options);
    }
}