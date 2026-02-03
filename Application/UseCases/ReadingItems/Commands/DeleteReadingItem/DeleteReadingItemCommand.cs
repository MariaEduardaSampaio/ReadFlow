using MediatR;

namespace Application.UseCases.ReadingItems.Commands.DeleteReadingItem;

public sealed record DeleteReadingItemCommand: IRequest<DeleteReadingItemResponse>
{
    public Guid BookId { get; init; }
    public Guid UserId { get; init; }
}