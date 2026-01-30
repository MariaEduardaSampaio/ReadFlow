using Application.Abstractions.Mappers;
using Application.Abstractions.Persistence;
using MediatR;

namespace Application.UseCases.ReadingItems.Queries.GetReadingItemsByStatuses;

public class GetReadingItemsByStatusesHandler(IReadingItemRepository repository):
    IRequestHandler<GetReadingItemsByStatusesQuery, GetReadingItemsByStatusesResponse>
{
    public async Task<GetReadingItemsByStatusesResponse> Handle(GetReadingItemsByStatusesQuery query, CancellationToken cancellationToken)
    {
        var grouped = await repository.GetReadingItemsByStatusAsync(query.Id, query.Statuses, cancellationToken);

        var groups = grouped
            .OrderBy(g => g.Key)
            .Select(g => new ReadingItemsByStatusDto(
                Status: g.Key.ToString(),
                Count: g.Count(),
                Items: g.Select(x => x.MapToDto()).ToList()
            ))
            .ToList();

        var total = groups.Sum(g => g.Count);

        return new GetReadingItemsByStatusesResponse(groups, total);
    }
}