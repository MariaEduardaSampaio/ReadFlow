using Application.Abstractions.Persistence;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.Users.Queries.GetUserById;

public sealed class GetUserByIdHandler(IUserRepository repository): IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(query.Id, cancellationToken);

        return user == null 
            ? throw new NotFoundException(nameof(User), query.Id) 
            : new GetUserByIdResponse(user.Id, user.Name.Text, user.Email.Address, user.Role.ToString());
    }
}