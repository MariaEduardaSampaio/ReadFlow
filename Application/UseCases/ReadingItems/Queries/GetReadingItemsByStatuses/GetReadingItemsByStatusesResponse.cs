using Application.Abstractions.TransferObjects;

namespace Application.UseCases.ReadingItems.Queries.GetReadingItemsByStatuses;

public sealed record GetReadingItemsByStatusesResponse(IReadOnlyCollection<ReadingItemsByStatusDto> Groups, int TotalCount);

public sealed record ReadingItemsByStatusDto(string Status, int Count, IReadOnlyCollection<ReadingItemDto> Items);