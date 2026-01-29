using System.Net.Mime;
using Application.UseCases.ReadingItems.Commands.CreateReadingItem;
using Application.UseCases.ReadingItems.Queries.GetReadingItemFromUser;
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
    
    [HttpPost("get-from-user")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReadingItemFromUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetReadingItemFromUserResponse> GetReadingItemFromUser([FromBody] GetReadingItemFromUserQuery query, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        
        return result;
    }
}