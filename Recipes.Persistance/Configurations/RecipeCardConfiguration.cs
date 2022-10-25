using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Entities;

namespace Recipes.Persistance.Configurations
{
    public class RecipeCardConfiguration : IEntityTypeConfiguration<RecipeCard>
    {
        public void Configure(EntityTypeBuilder<RecipeCard> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()");

            builder.OwnsOne(x => x.Image, u =>
            {
                u.WithOwner()
                    .HasForeignKey("RecipeCardId");
                u.Property("RecipeCardId")
                    .HasColumnName("RecipeCardId");

                u.Property(x => x.Content)
                    .HasColumnName("Content")
                    .IsRequired();

                u.Property(x => x.ContentType)
                    .HasColumnName("ContentType")
                    .IsRequired();

                u.Property(x => x.Size)
                    .HasColumnName("Size")
                    .HasColumnType("bigint")
                    .IsRequired();

                u.Property(x => x.FileName)
                    .HasColumnName("FileName");

                u.ToTable("RecipeMainImages");
            });
            builder.Ignore(x => x.Events);

            builder.HasOne(x => x.Details)
                .WithOne();

            builder.ToTable("RecipeCards");
        }
    }
}
