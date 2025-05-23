using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinAppApi.Entities;

namespace MinAppApi.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(e => e.Title).IsRequired(true).HasMaxLength(150);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.HasOne(e => e.Organizer).WithMany(t => t.Events).HasForeignKey(e => e.OrganizerId);
            builder.Property(e => e.Location).IsRequired(true).HasMaxLength(200);
            builder.Property(e => e.Date).IsRequired(true);

        }
    }
}
