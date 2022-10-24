using MediatR;
using Recipes.Contracts;

namespace Recipes.Application.UseCases.Users.Commands.CreateUser
{
    public record CreateUserCommand(UserDto User) : IRequest<UserDto>;
    
}
