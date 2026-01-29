using Application.Abstractions.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.ReadingItems.Commands.CreateReadingItem;

public sealed class CreateReadingItemHandler(IReadingItemRepository repository): IRequestHandler<CreateReadingItemCommand, CreateReadingItemResponse>
{
    public async Task<CreateReadingItemResponse> Handle(CreateReadingItemCommand command, CancellationToken cancellationToken)
    {
        var alreadyExists = await repository.ExistsAsync(command.BookId, command.UserId, cancellationToken);
        
        if (alreadyExists)
        {
            throw new InvalidOperationException("Reading item for this book and user already exists.");
        }
        
        var readingItem = new ReadingItem(id: Guid.CreateVersion7(),
            bookId: command.BookId,
            userId: command.UserId,
            status: command.Status,
            createdAt: DateTime.UtcNow,
            currentPage: command.CurrentPage,
            review: command.Review,
            rating: command.Rating,
            readingStartedAt: command.ReadingStartedAt,
            readingFinishedAt: command.ReadingFinishedAt
        );
        
        await repository.AddAsync(readingItem, cancellationToken);

        await repository.UnitOfWork.CommitAsync(cancellationToken);

        return new CreateReadingItemResponse(readingItem.Id);
    }
}