using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Entities;

namespace Recipes.Persistance.Configurations
{
    public class RecipeCardDetailsConfiguration : IEntityTypeConfiguration<RecipeCardDetails>
    {
        public void Configure(EntityTypeBuilder<RecipeCardDetails> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsMany(x => x.Hashtags, u =>
            {
                u.Property(x => x.Title)
                    .IsRequired()
                    .HasColumnName("Title");
                
                u.HasIndex(x => x.Title);
                builder.ToTable("Hashtags");
            });

            builder.OwnsMany(x => x.Ingredients, u =>
            {
                u.Property(x => x.Name)
                    .IsRequired()
                    .HasColumnName("Name");
                u.Property(x => x.Quantity)
                    .IsRequired()
                    .HasColumnName("Quantity");

                u.HasIndex(x => x.Name);
                u.ToTable("Ingredients");
            });

            builder.Navigation(x => x.Hashtags)
                .HasField("_hashtags")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(x => x.Ingredients)
                .HasField("_ingredients")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.ToTable("RecipeCardDetails");
        }
    }
}
