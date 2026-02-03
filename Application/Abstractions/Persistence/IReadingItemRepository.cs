using Domain.Entities;
using Domain.Enums;

namespace Application.Abstractions.Persistence;

public interface IReadingItemRepository
{
    IUnitOfWork UnitOfWork { get; }
    
    Task AddAsync(ReadingItem readingItem, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid bookId, Guid userId, CancellationToken cancellationToken);
    Task<ReadingItem?> GetByIdAsync(Guid bookId, Guid userId, CancellationToken cancellationToken);
    Task<List<IGrouping<EReadingStatus, ReadingItem>>> GetReadingItemsByStatusAsync(Guid id, IEnumerable<EReadingStatus>? statuses,
        CancellationToken cancellationToken);
    void Update(ReadingItem readingItem);
    void Delete(ReadingItem readingItem);
}