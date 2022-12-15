using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Persistance.Outbox;

namespace Recipes.Persistance.Configurations
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProccededOn)
                .IsRequired(false);

            builder.Property(x => x.Error)
                .IsRequired(false);

            builder.ToTable("OutboxMessages");
        }
    }
}
