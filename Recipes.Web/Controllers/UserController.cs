using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.Users.Commands.CreateUser;
using Recipes.Application.UseCases.Users.Commands.SignIn;
using Recipes.Contracts;
using Recipes.Contracts.Auth;
using Recipes.Contracts.Web;

namespace Recipes.Web.Controllers
{
    [Route("api/users")]
    public class UserController : ApplicationController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("sign-up")]
        public async Task<Response<UserDto>> CreateUser([FromBody] SignUpUserDto userDto, CancellationToken token)
        {
            return Created(await Mediator.Send(new SignUpUserCommand(userDto), token));
        }

        [HttpPost("sign-in")]
        public async Task<Response<UserDto>> FindUser([FromBody] SignInUserDto userDto, CancellationToken token)
        {
            return Ok(await Mediator.Send(new SignInUserCommand(userDto), token));
        }
    }
}
