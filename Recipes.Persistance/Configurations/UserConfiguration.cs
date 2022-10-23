using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Entities;

namespace Recipes.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Events);
            builder.ToTable("Users");

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Login)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property<string>("_salt")
                .HasColumnName("Salt")
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
