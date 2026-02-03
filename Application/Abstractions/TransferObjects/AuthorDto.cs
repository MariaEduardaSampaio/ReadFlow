namespace Application.Abstractions.TransferObjects;

public sealed record AuthorDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}