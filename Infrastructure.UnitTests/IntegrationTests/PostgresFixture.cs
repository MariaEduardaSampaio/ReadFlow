using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Npgsql;

namespace Infrastructure.UnitTests.IntegrationTests;

public sealed class PostgresFixture : IAsyncDisposable
{
    private readonly IContainer _container;

    public string ConnectionString { get; }

    public PostgresFixture()
    {
        _container = new ContainerBuilder()
            .WithImage("postgres:16")
            .WithEnvironment("POSTGRES_DB", "readflow_test")
            .WithEnvironment("POSTGRES_USER", "postgres")
            .WithEnvironment("POSTGRES_PASSWORD", "postgres")
            .WithPortBinding(5432, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(5432))
            .Build();

        ConnectionString = string.Empty;
    }

    public async Task StartAsync()
    {
        await _container.StartAsync();

        var hostPort = _container.GetMappedPublicPort(5432);

        var csb = new NpgsqlConnectionStringBuilder
        {
            Host = "localhost",
            Port = hostPort,
            Database = "readflow_test",
            Username = "postgres",
            Password = "postgres",
            IncludeErrorDetail = true
        };

        _connectionString = csb.ConnectionString;
    }

    private string _connectionString = "";
    public string GetConnectionString() => _connectionString;

    public async ValueTask DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}