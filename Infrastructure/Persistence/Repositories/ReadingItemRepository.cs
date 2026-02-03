using Application.Abstractions.Persistence;
using Domain.Entities;
using Domain.Enums;
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
    
    public void Update(ReadingItem readingItem)
    {
        dbContext.ReadingItems.Update(readingItem);
    }
    
    public void Delete(ReadingItem readingItem)
    {
        dbContext.ReadingItems.Remove(readingItem);
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
    
    public async Task<List<IGrouping<EReadingStatus, ReadingItem>>> GetReadingItemsByStatusAsync(Guid id,
        IEnumerable<EReadingStatus>? statuses, CancellationToken cancellationToken)
    {
        var query = dbContext.ReadingItems
            .Where(r => r.UserId == id)
            .Include(r => r.User)
            .Include(r => r.Book).ThenInclude(b => b.Categories)
            .Include(r => r.Book).ThenInclude(b => b.Authors)
            .AsQueryable();

        var normalized = statuses?.Distinct().ToArray();

        if (normalized is { Length: > 0 })
        {
            query = query.Where(r => Enumerable.Contains(normalized, r.Status));
        }

        return await query
            .GroupBy(r => r.Status)
            .ToListAsync(cancellationToken);
    }
}