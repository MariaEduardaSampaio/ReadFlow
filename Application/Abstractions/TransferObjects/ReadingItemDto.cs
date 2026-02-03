using Domain.Enums;

namespace Application.Abstractions.TransferObjects;

public sealed record ReadingItemDto
{
    public Guid Id { get; init; }
    public EReadingStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public int? CurrentPage { get; init; }
    public string? Review { get; init; }
    public int Rating { get; init; }
    public DateTime? ReadingStartedAt { get; init; }
    public DateTime? ReadingFinishedAt { get; init; }
    public BookDto Book { get; init; }
    public UserDto User { get; init; }
}