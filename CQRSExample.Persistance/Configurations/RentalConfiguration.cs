using CQRSExample.Domain.Entities;
using CQRSExample.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRSExample.Persistance.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.Property(e => e.Id).HasColumnName("RentalID");
            builder.Property(e => e.DateOut).IsRequired();
            builder.Property(e => e.RentalFee).HasPrecision(8, 2);
        }
    }
} 