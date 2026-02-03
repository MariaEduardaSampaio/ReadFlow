using Application.Abstractions.Persistence;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.ReadingItems.Commands.DeleteReadingItem;

public sealed class DeleteReadingItemHandler(IReadingItemRepository repository):
    IRequestHandler<DeleteReadingItemCommand, DeleteReadingItemResponse>
{
    public async Task<DeleteReadingItemResponse> Handle(DeleteReadingItemCommand command, CancellationToken cancellationToken)
    {
        var readingItem = await repository.GetByIdAsync(command.BookId, command.UserId, cancellationToken);
        
        if (readingItem == null)
        {
            throw new NotFoundException(nameof(ReadingItem), "Reading item for this book and user does not exist.");
        }

        repository.Delete(readingItem);

        await repository.UnitOfWork.CommitAsync(cancellationToken);

        return new DeleteReadingItemResponse(true);
        
    }
}