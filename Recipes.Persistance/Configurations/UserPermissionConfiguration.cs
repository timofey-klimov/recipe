using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Entities;

namespace Recipes.Persistance.Configurations
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Permission);
            builder.Property(x => x.IssuedAt)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()");
            builder.ToTable("UserPermissions");
        }
    }
}
