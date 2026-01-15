namespace Application.UseCases.Users.Queries.GetUserById;

public sealed record GetUserByIdResponse(Guid Id, string Name, string Email, string Role);