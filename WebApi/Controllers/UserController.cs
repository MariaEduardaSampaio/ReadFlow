using System.Net.Mime;
using Application.UseCases.Users.Commands.CreateUser;
using Application.UseCases.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(ISender sender)
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<CreateUserResponse> CreateUser([FromBody] CreateUserCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return result;
    }
    
    [HttpGet("{id:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserByIdResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUserByIdResponse> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        
        var result = await sender.Send(query, cancellationToken);
        
        return result;
    }
}