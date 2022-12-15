using Recipes.Domain.Core.Repositories;
using Recipes.Domain.Entities;

namespace Recipes.Domain.Repositories
{
    public interface IPermissionRepository : IAggregateRootRepository<Permission, int>
    {
        Task<Permission?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    }
}
