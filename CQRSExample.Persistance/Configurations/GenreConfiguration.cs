using CQRSExample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRSExample.Persistance.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(e => e.Id).HasColumnName("GenreID");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}