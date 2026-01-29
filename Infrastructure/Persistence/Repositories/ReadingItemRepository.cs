using Application.Abstractions.Persistence;
using Domain.Entities;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ReadingItemRepository(ReadFlowDbContext dbContext): IReadingItemRepository
{
    public IUnitOfWork UnitOfWork => dbContext;
    
    public async Task AddAsync(ReadingItem readingItem, CancellationToken cancellationToken)
    {
        await dbContext.ReadingItems.AddAsync(readingItem, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid bookId, Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.ReadingItems
            .AnyAsync(r => r.BookId == bookId && r.UserId == userId, cancellationToken);
    }

    public async Task<ReadingItem?> GetByIdAsync(Guid bookId, Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.ReadingItems
            .Include(r => r.User)
            .Include(r => r.Book)
                .ThenInclude(b => b.Categories)
            .Include(r => r.Book)
                .ThenInclude(b => b.Authors)
            .FirstOrDefaultAsync(r => r.BookId == bookId && r.UserId == userId, cancellationToken);
    }
}