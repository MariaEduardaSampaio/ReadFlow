using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public Title Title { get; private set; }
    public Synopsis Synopsis { get; private set; }
    public Isbn Isbn { get; private set; }
    public int PageCount { get; private set; }
    public string Publisher { get; private set; }
    public DateTime PublishedDate { get; private set; }
    public ICollection<Author> Authors { get; private set; } = [];
    public ICollection<Category> Categories { get; private set; } = [];
    public ICollection<ReadingItem> ReadingItems { get; private set; } = [];
    
    protected Book() { }

    public Book(Guid id, string title, string synopsis, string isbn, int pageCount, string publisher, DateTime publishedDate)
    {
        Validate(pageCount);
        
        Id = id;
        Title = new Title(title);
        Synopsis = new Synopsis(synopsis);
        Isbn = new Isbn(isbn);
        PageCount = pageCount;
        Publisher = publisher;
        PublishedDate = publishedDate;
    }

    private static void Validate(int pageCount)
    {
        if (pageCount <= 0)
        {
            throw new RuleViolationDomainException("Page count must be greater than 0.");
        }
    }
}