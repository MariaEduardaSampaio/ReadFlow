using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class ReadingItem
{
    public Guid Id { get; private set; }
    public EReadingStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int? CurrentPage { get; private set; }
    public Review Review { get; private set; }
    public Rating Rating { get; private set; }
    public DateTime? ReadingStartedAt { get; private set; }
    public DateTime? ReadingFinishedAt { get; private set; }
    public Book Book { get; private set; }
    public Guid BookId { get; private set; }
    public User User { get; private set; }
    public Guid UserId { get; private set; }
    

    protected ReadingItem() { }

    public ReadingItem(Guid id, Guid bookId, Guid userId, EReadingStatus status, DateTime createdAt, int? currentPage, 
        string? review, int? rating, DateTime? readingStartedAt, DateTime? readingFinishedAt)
    {
        ValidateCurrentPage(currentPage);
        ValidateReadingInterval(readingStartedAt, readingFinishedAt);
        
        Id = id;
        BookId = bookId;
        UserId = userId;
        Status = status;
        CreatedAt = createdAt;
        CurrentPage = currentPage;
        Review = new Review(review);
        Rating = new Rating(rating);
        ReadingStartedAt = readingStartedAt;
        ReadingFinishedAt = readingFinishedAt;
    }
    
    public void SetCurrentPage(int? currentPage)
    {
        ValidateCurrentPage(currentPage);

        CurrentPage = currentPage;
    }
    
    public void SetReadingInterval(DateTime? readingStartedAt, DateTime? readingFinishedAt)
    {
        ValidateReadingInterval(readingStartedAt, readingFinishedAt);
        
        ReadingStartedAt = readingStartedAt;
        ReadingFinishedAt = readingFinishedAt;
    }
    
    public void SetReview(string? review)
    {
        Review = new Review(review);
    }
    
    public void SetRating(int? rating)
    {
        Rating = new Rating(rating);
    }
    
    public void SetStatus(EReadingStatus status)
    {
        Status = status;
    }

    private static void ValidateReadingInterval(DateTime? readingStartedAt, DateTime? readingFinishedAt)
    {
        if (readingStartedAt.HasValue && readingFinishedAt.HasValue
                                     && readingStartedAt.Value > readingFinishedAt.Value)
        {
            var message = $"Start date ({readingStartedAt:dd-MM-yyyy}) cannot be later than end date ({readingFinishedAt:dd-MM-yyyy}).";
            throw new RuleViolationDomainException(message);
        }
    }
    
    private void ValidateCurrentPage(int? currentPage)
    {
        if (currentPage.GetValueOrDefault() < default(int))
        {
            throw new RuleViolationDomainException("Current page cannot be less than 0.");
        }
        
        if (Book!= null && Book.PageCount < currentPage.GetValueOrDefault())
        {
            throw new RuleViolationDomainException("Current page cannot be greater than the total number of pages in the book.");
        }
    }
}
