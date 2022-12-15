using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;
using System.Collections.Immutable;

namespace Recipes.Domain.Repositories
{
    public interface IUserRepository : IAggregateRootRepository<User, int>
    {
        Task<bool> IsUserLoginExistsAsync(string login, CancellationToken token = default);

        Task<bool> IsUserEmailExistsAsync(string email, CancellationToken token = default);

        Task<User?> GetUserByEmailOrLoginAsync(string? login, string? email, CancellationToken token = default);

        Task<User?> GetByIdWithFavouriteRecipesAsync(int id, CancellationToken token = default);

        Task<HashSet<string>> GetPermissionsAsync(int id, CancellationToken token = default);
    }
}
