using Application.Abstractions.Persistence;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Users.Commands.CreateUser;

public sealed class CreateUserHandler(IUserRepository repository): IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<CreateUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = new User(id: Guid.NewGuid(), name: command.Name, email: command.Email, role: EUserRole.User);
        
        await repository.AddAsync(user, cancellationToken);

        await repository.UnitOfWork.CommitAsync(cancellationToken);

        return new CreateUserResponse(user.Id);
    }
}