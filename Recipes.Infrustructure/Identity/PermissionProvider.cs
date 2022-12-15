using Recipes.Application.Core.Auth;
using Recipes.Application.Core.Identity;
using Recipes.Domain.Repositories;
using Permissions = Recipes.Application.Core.Identity.Enums.Permissions;

namespace Recipes.Infrustructure.Identity
{
    public class PermissionProvider : IPermissionProvider
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IUserRepository _userRepository;
        public PermissionProvider(ICurrentUserProvider currentUserProvider, IUserRepository userRepository)
        {
            _currentUserProvider = currentUserProvider;
            _userRepository = userRepository;
            
        }

        public Task<bool> HasPermissionsAsync(IEnumerable<Permissions> permissions, bool includeAll = false)
        {
            var requiredPermissions = permissions.Select(x => x.ToString()).ToArray();
            return InternalHasPermissions(_currentUserProvider.UserId, requiredPermissions, includeAll);
        }

        public Task<bool> HasPermissionsAsync(int? id, IEnumerable<Permissions> permissions, bool includeAll = false)
        {
            var requiredPermissions = permissions.Select(x => x.ToString()).ToArray();
            return InternalHasPermissions(id, requiredPermissions, includeAll);
        }

        private async Task<bool> InternalHasPermissions(
            int? userId, IEnumerable<string> requiredPermissions, bool includeAll)
        {
            if (userId == null)
                return false;

            var userPermissions =
               await _userRepository.GetPermissionsAsync(userId.Value, CancellationToken.None);

            if (userPermissions == null || userPermissions.Count == 0)
                return false;

            if (!includeAll)
            {
                return requiredPermissions.Intersect(userPermissions).Any();
            }
            else
            {
                foreach (var permission in requiredPermissions)
                {
                    if (!userPermissions.Contains(permission))
                        return false;
                }

                return true;
            }
        }
    }
}
