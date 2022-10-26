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

            builder.OwnsOne(x => x.Image, u =>
            {
                u.WithOwner()
                    .HasForeignKey("CookingStageId");
                u.Property("CookingStageId")
                    .HasColumnName("CookingStageId");

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

                u.ToTable("CookingStageImages");
            });

            builder.Property(x => x.Description)
                .IsRequired();

            builder.ToTable("CookingStages");
        }
    }
}
