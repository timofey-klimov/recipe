using Recipes.Domain.Entities;

namespace Recipes.Application.Core.Auth
{
    public interface IJwtTokenProvider
    {
        string CreateToken(User user);
    }
}
