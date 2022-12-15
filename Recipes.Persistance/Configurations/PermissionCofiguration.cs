using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Application.Core.Identity;
using Recipes.Application.Core.Identity.Enums;
using Recipes.Domain.Entities;

namespace Recipes.Persistance.Configurations
{
    public class PermissionCofiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);

            var seed = Enum.GetValues<Permissions>()
                .Select(permission => new Permission((int)permission, permission.ToString()));

            builder.HasData(seed);

            builder.ToTable("Permissions");
        }
    }
}
