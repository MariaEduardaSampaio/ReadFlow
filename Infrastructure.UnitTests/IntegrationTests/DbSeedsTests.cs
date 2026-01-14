using FluentAssertions;
using Infrastructure.EntityFramework.Seeds;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.UnitTests.IntegrationTests;

[TestFixture]
public sealed class DbSeedsTests
{
    private PostgresFixture _postgresFixture = null!;
    private string _connectionString = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        _postgresFixture = new PostgresFixture();
        await _postgresFixture.StartAsync();
        _connectionString = _postgresFixture.GetConnectionString();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _postgresFixture.DisposeAsync();
    }
    
    [Test]
    public async Task Migrate_ShouldCreateSchema()
    {
        await using var db = DbContextFactory.Create(_connectionString);

        await db.Database.MigrateAsync();

        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = conn.CreateCommand();
        cmd.CommandText = """
                              SELECT COUNT(*)
                              FROM information_schema.tables
                              WHERE table_name = '__EFMigrationsHistory';
                          """;

        var result = await cmd.ExecuteScalarAsync();
        var count = Convert.ToInt32(result);

        count.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task Seed_ShouldInsertCategoriesAndBooks()
    {
        await using var db = DbContextFactory.Create(_connectionString);

        await DatabaseSeeder.SeedAsync(db);

        (await db.Categories.CountAsync()).Should().BeGreaterThan(0);
        (await db.Books.CountAsync()).Should().Be(4);
        (await db.Authors.CountAsync()).Should().Be(5);
    }

    [Test]
    public async Task Seed_WhenRunTwice_ShouldNotDuplicate()
    {
        await using var db = DbContextFactory.Create(_connectionString);

        await DatabaseSeeder.SeedAsync(db);
        var cats1 = await db.Categories.CountAsync();
        var books1 = await db.Books.CountAsync();

        await DatabaseSeeder.SeedAsync(db);
        var cats2 = await db.Categories.CountAsync();
        var books2 = await db.Books.CountAsync();

        cats2.Should().Be(cats1);
        books2.Should().Be(books1);
    }

    [Test]
    public async Task CategoryName_ShouldBeUnique()
    {
        await using var db = DbContextFactory.Create(_connectionString);
        await db.Database.MigrateAsync();

        db.Categories.Add(new Domain.Entities.Category(Guid.NewGuid(), "Fantasy"));
        await db.SaveChangesAsync();

        db.Categories.Add(new Domain.Entities.Category(Guid.NewGuid(), "Fantasy"));

        var act = async () => await db.SaveChangesAsync();

        // EF encapsula PostgresException dentro DbUpdateException
        var ex = await act.Should().ThrowAsync<DbUpdateException>();
        ex.Which.InnerException.Should().BeOfType<PostgresException>()
            .Which.SqlState.Should().Be("23505");
    }
}
