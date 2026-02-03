using MediatR;

namespace Application.UseCases.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Name, string Email): IRequest<CreateUserResponse>;