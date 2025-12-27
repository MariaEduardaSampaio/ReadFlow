namespace Domain.Aggregates.Books;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Synopsis { get; private set; }
    public string? Isbn { get; private set; }
    public int PageCount { get; private set; }
    public string Publisher { get; private set; }
    public DateTime PublishedDate { get; private set; }
    
    protected Book() { }

    public Book(Guid id, string title, string synopsis, string? isbn, int pageCount, string publisher, DateTime publishedDate)
    {
        Id = id;
        Title = title;
        Synopsis = synopsis;
        Isbn = isbn;
        PageCount = pageCount;
        Publisher = publisher;
        PublishedDate = publishedDate;
    }
}