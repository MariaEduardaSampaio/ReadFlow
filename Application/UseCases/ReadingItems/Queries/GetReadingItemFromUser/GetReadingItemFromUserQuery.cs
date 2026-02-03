using MediatR;

namespace Application.UseCases.ReadingItems.Queries.GetReadingItemFromUser;

public sealed record GetReadingItemFromUserQuery(Guid BookId, Guid UserId): IRequest<GetReadingItemFromUserResponse>;