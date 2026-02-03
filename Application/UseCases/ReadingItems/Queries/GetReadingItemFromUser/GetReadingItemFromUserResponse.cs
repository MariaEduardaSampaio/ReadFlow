using Application.Abstractions.TransferObjects;

namespace Application.UseCases.ReadingItems.Queries.GetReadingItemFromUser;

public sealed record GetReadingItemFromUserResponse(ReadingItemDto ReadingItem);