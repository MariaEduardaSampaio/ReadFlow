using Application.Abstractions.Persistence;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.ReadingItems.Commands.UpdateReadingItem;

public sealed class UpdateReadingItemHandler(IReadingItemRepository repository):
    IRequestHandler<UpdateReadingItemCommand, UpdateReadingItemResponse>
{
    public async Task<UpdateReadingItemResponse> Handle(UpdateReadingItemCommand command, CancellationToken cancellationToken)
    {
        var readingItem = await repository.GetByIdAsync(command.BookId, command.UserId, cancellationToken);
        
        if (readingItem == null)
        {
            throw new NotFoundException(nameof(ReadingItem), "Reading item for this book and user does not exist.");
        }

        UpdateReadingItem(readingItem, command);
        
        repository.Update(readingItem);

        await repository.UnitOfWork.CommitAsync(cancellationToken);

        return new UpdateReadingItemResponse(true);
    }

    private static void UpdateReadingItem(ReadingItem readingItem, UpdateReadingItemCommand command)
    {
        readingItem.SetStatus(command.Status);
        readingItem.SetCurrentPage(command.CurrentPage);
        readingItem.SetReview(command.Review);
        readingItem.SetRating(command.Rating);
        readingItem.SetReadingInterval(command.ReadingStartedAt, command.ReadingFinishedAt);
    }
}