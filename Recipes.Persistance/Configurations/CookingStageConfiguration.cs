using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Entities;

namespace Recipes.Persistance.Configurations
{
    public class CookingStageConfiguration : IEntityTypeConfiguration<CookingStage>
    {
        public void Configure(EntityTypeBuilder<CookingStage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.ToTable("CookingStages");
        }
    }
}
