using System.Net.Mime;
using Application.UseCases.Users.Commands.CreateUser;
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
    public async Task<CreateUserResponse> SendConfirmation([FromBody] CreateUserCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        
        return result;
    }
}