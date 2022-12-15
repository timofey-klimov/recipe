using Recipes.Domain.Core;

namespace Recipes.Domain.Entities
{
    public class UserPermission : Entity<int>
    {
        public DateTime IssuedAt { get; private set; }

        public Permission Permission { get; private set; }

        public UserPermission(Permission permission)
        {
            Permission = permission;
        }

        private UserPermission() { }
    }
}
