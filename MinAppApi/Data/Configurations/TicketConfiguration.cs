using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinAppApi.Entities;

namespace MinAppApi.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(e => e.Type).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.Price).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(e => e.QuantityAvailable).IsRequired(true);
        }
    }
}
