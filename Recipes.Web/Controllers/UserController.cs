using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.UseCases.Users.Commands.CreateUser;
using Recipes.Application.UseCases.Users.Commands.SignIn;
using Recipes.Application.UseCases.Users.Queries.CheckUserExists;
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

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>

        [HttpPost("sign-up")]
        public async Task<Response<string>> CreateUser([FromBody] SignUpUserDto userDto, CancellationToken token)
        {
            return Created(await Mediator.Send(new SignUpUserCommand(userDto), token));
        }

        /// <summary>
        /// Войти
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>

        [HttpPost("sign-in")]
        public async Task<Response<string>> FindUser([FromBody] SignInUserDto userDto, CancellationToken token)
        {
            return Ok(await Mediator.Send(new SignInUserCommand(userDto), token));
        }

        /// <summary>
        /// Проверка на наличие пользователя по email/логин
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="token"></param>
        /// <returns></returns>

        [HttpGet("check/{userInfo}")]
        public async Task<Response<bool>> CheckUserExists(string userInfo, CancellationToken token)
        {
            return Ok(await Mediator.Send(new CheckUserExistsQuery(userInfo), token));
        }
    }
}
