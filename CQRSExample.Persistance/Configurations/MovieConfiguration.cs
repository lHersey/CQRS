using CQRSExample.Domain.Entities;
using CQRSExample.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRSExample.Persistance.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(e => e.Id).HasColumnName("MovieID");
            builder.Property(e => e.Title).IsRequired().HasMaxLength(15);
            builder.Property(e => e.DailyRentalRate).HasPrecision(8, 2);
        }
    }
}