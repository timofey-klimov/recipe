using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;

namespace Recipes.Domain.Repositories
{
    public interface IUserRepository : IAggregateRootRepository<User>
    {
        Task<bool> IsUserLoginExistsAsync(string login, CancellationToken token = default);

        Task<bool> IsUserEmailExistsAsync(string email, CancellationToken token = default);
    }
}
