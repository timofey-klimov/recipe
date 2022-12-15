using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Entities;
using Recipes.Domain.Enumerations;

namespace Recipes.Persistance.Configurations
{
    public class ConfirmationRequestConfiguration : IEntityTypeConfiguration<ConfirmationRequest>
    {
        public void Configure(EntityTypeBuilder<ConfirmationRequest> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasOne(typeof(RecipeCard))
                .WithMany()
                .HasForeignKey("RecipeId");

            builder.HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("CreatedBy")
                .IsRequired(false);

            builder.HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("AcceptedBy")
                .IsRequired(false);

            builder.HasOne(typeof(User))
                .WithMany()
                .HasForeignKey("RejectedBy")
                .IsRequired(false);

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.Status)
                .HasConversion(
                    x => x.Value,
                    u => ConfirmationRequestStatus.FromValue(u));

            builder.Property(x => x.CheckType)
                .HasConversion(
                    x => x.Value,
                    u => ConfirmationRequestCheckType.FromValue(u));

            builder.Property(x => x.RejectedReason)
                .IsRequired(false);

            builder.ToTable("ConfirmationRequests");
        }
    }
}
