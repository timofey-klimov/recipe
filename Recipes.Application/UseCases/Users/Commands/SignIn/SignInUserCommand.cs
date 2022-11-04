using MediatR;
using Recipes.Contracts;
using Recipes.Contracts.Auth;

namespace Recipes.Application.UseCases.Users.Commands.SignIn
{
    public record SignInUserCommand(SignInUserDto User) : IRequest<string>;
}
