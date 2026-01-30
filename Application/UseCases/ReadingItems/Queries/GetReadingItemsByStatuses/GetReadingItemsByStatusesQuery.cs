using Domain.Enums;
using MediatR;

namespace Application.UseCases.ReadingItems.Queries.GetReadingItemsByStatuses;

public sealed record GetReadingItemsByStatusesQuery(Guid Id, IEnumerable<EReadingStatus>? Statuses = null): IRequest<GetReadingItemsByStatusesResponse>;