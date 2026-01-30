namespace Application.Abstractions.TransferObjects;

public sealed record UserDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
}