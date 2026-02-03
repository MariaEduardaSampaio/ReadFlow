using System.Net.Mime;
using Application.UseCases.ReadingItems.Commands.CreateReadingItem;
using Application.UseCases.ReadingItems.Commands.DeleteReadingItem;
using Application.UseCases.ReadingItems.Commands.UpdateReadingItem;
using Application.UseCases.ReadingItems.Queries.GetReadingItemFromUser;
using Application.UseCases.ReadingItems.Queries.GetReadingItemsByStatuses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/reading-items")]
public class ReadingItemsController(ISender sender)
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateReadingItemResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<CreateReadingItemResponse> CreateReadingItem([FromBody] CreateReadingItemCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return result;
    }
    
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateReadingItemResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<UpdateReadingItemResponse> UpdateReadingItem([FromBody] UpdateReadingItemCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return result;
    }
    
    [HttpDelete]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteReadingItemResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DeleteReadingItemResponse> DeleteReadingItem([FromBody] DeleteReadingItemCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return result;
    }
    
    [HttpPost("item")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReadingItemFromUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetReadingItemFromUserResponse> GetReadingItemFromUser([FromBody] GetReadingItemFromUserQuery query, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        
        return result;
    }
    
    [HttpGet("summary")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReadingItemsByStatusesResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetReadingItemsByStatusesResponse> GetReadingItemsByStatuses(GetReadingItemsByStatusesQuery query, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        
        return result;
    }
}