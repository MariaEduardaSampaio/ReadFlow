using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Seeds;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ReadFlowDbContext db, CancellationToken ct = default)
    {
        await db.Database.MigrateAsync(ct);

        var (authors, books, categories) = SeedData.Create();

        // Categories
        if (!await db.Categories.AnyAsync(ct))
        {
            await db.Categories.AddRangeAsync(categories, ct);
            await db.SaveChangesAsync(ct);
        }

        // Authors
        if (!await db.Authors.AnyAsync(ct))
        {
            await db.Authors.AddRangeAsync(authors, ct);
            await db.SaveChangesAsync(ct);
        }

        // Books with Relationships
        if (!await db.Books.AnyAsync(ct))
        {
            await db.Books.AddRangeAsync(books, ct);
            await db.SaveChangesAsync(ct);
        }
    }
}