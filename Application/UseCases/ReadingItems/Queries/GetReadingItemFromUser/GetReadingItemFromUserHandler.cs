using Application.Abstractions.Mappers;
using Application.Abstractions.Persistence;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.ReadingItems.Queries.GetReadingItemFromUser;

public sealed class GetReadingItemFromUserHandler(IReadingItemRepository repository):
    IRequestHandler<GetReadingItemFromUserQuery, GetReadingItemFromUserResponse>
{
    public async Task<GetReadingItemFromUserResponse> Handle(GetReadingItemFromUserQuery query, CancellationToken cancellationToken)
    {
        var readingItem = await repository.GetByIdAsync(query.BookId, query.UserId, cancellationToken);
        
        return readingItem == null 
            ? throw new NotFoundException(nameof(ReadingItem)) 
            : new GetReadingItemFromUserResponse(readingItem.MapToDto());
    }
}