using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.Users.Commands.CreateUser;
using Recipes.Contracts;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    [Route("api/users")]
    public class UserController : ApplicationController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<Response<UserDto>> CreateUser([FromBody] UserDto userDto, CancellationToken token)
        {
            return Created(await Mediator.Send(new CreateUserCommand(userDto), token));
        }
    }
}
