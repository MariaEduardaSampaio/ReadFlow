using Application.Abstractions.TransferObjects;

namespace Application.UseCases.Users.Queries.GetUserById;

public sealed record GetUserByIdResponse(UserDto User);