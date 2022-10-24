using MediatR;
using Recipes.Contracts;
using Recipes.Contracts.Auth;

namespace Recipes.Application.UseCases.Users.Commands.CreateUser
{
    public record SignUpUserCommand(SignUpUserDto User) : IRequest<UserDto>;
    
}
