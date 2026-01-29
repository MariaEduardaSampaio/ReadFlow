using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface IReadingItemRepository
{
    IUnitOfWork UnitOfWork { get; }
    
    Task AddAsync(ReadingItem readingItem, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid bookId, Guid userId, CancellationToken cancellationToken);
    Task<ReadingItem?> GetByIdAsync(Guid bookId, Guid userId, CancellationToken cancellationToken);
}