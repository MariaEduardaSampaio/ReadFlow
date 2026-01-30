using Domain.Enums;
using MediatR;

namespace Application.UseCases.ReadingItems.Commands.UpdateReadingItem;

public sealed record UpdateReadingItemCommand: IRequest<UpdateReadingItemResponse>
{
    public Guid BookId { get; init; }
    public Guid UserId { get; init; }
    public EReadingStatus Status { get; init; }
    public int? CurrentPage { get; init; }
    public string? Review { get; init; }
    public int? Rating { get; init; }
    public DateTime? ReadingStartedAt { get; init; }
    public DateTime? ReadingFinishedAt { get; init; }
}