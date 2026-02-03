using MediatR;

namespace Application.UseCases.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<GetUserByIdResponse>;