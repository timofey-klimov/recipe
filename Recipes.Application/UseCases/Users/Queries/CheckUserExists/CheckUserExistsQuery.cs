using MediatR;

namespace Recipes.Application.UseCases.Users.Queries.CheckUserExists
{
    public record CheckUserExistsQuery(string UserInfo) : IRequest<bool>;
    
}
