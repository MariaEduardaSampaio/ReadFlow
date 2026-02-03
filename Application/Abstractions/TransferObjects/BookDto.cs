namespace Application.Abstractions.TransferObjects;

public sealed record BookDto
{
    public Guid Id { get; init; }
    public int PageCount { get; init; }
    public string Title { get; init; }
    public string Synopsis { get; init; }
    public string Isbn { get; init; }
    public string Publisher { get; init; }
    public DateTime PublishedDate { get; init; }
    public IReadOnlyCollection<AuthorDto> Authors { get; init; }
    public IReadOnlyCollection<CategoryDto> Categories { get; init; }
}