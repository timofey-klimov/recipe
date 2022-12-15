using Recipes.Application.Core.Identity.Enums;

namespace Recipes.Application.Core.Identity
{
    public interface IPermissionProvider
    {
        Task<bool> HasPermissionsAsync(IEnumerable<Permissions> permissions, bool includeAll = false);

        Task<bool> HasPermissionsAsync(int? id, IEnumerable<Permissions> permissions, bool includeAll = false);
    }
}
