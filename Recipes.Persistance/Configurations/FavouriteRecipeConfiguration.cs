using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Entities;

namespace Recipes.Persistance.Configurations
{
    public class FavouriteRecipeConfiguration : IEntityTypeConfiguration<FavouriteRecipe>
    {
        public void Configure(EntityTypeBuilder<FavouriteRecipe> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LikeDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()");

            builder.ToTable("FavouriteRecipes");
        }
    }
}
