using Domain.Aggregates.ReadingItems.ValueObjects;
using Domain.Common.Enums;

namespace Domain.Aggregates.ReadingItems;

public class ReadingItem
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public Guid UserId { get; private set; }
    public EReadingStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int? CurrentPage { get; private set; }
    public string? Review { get; private set; }
    public Rating? Rating { get; private set; }
    public DateTime? BeginningToRead { get; private set; }
    public DateTime? EndedReading { get; private set; }

    protected ReadingItem() { }

    public ReadingItem(Guid id, Guid bookId, Guid userId, EReadingStatus status, DateTime createdAt, int? currentPage, 
        string? review, Rating? rating, DateTime? beginningToRead, DateTime? endedReading)
    {
        Id = id;
        BookId = bookId;
        UserId = userId;
        Status = status;
        CreatedAt = createdAt;
        CurrentPage = currentPage;
        Review = review;
        Rating = rating;
        BeginningToRead = beginningToRead;
        EndedReading = endedReading;
    }
}
