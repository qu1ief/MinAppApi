using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinAppApi.Entities;

namespace MinAppApi.Data.Configurations
{
    public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            builder.Property(e => e.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(e => e.Email).IsRequired(true);
            builder.Property(e => e.Phone).HasMaxLength(20);

        }
    }
}
