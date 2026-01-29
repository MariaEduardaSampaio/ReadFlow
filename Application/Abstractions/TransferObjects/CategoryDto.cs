namespace Application.Abstractions.TransferObjects;

public sealed record CategoryDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}