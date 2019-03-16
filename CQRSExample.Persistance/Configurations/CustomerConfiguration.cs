using CQRSExample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRSExample.Persistance.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(p => p.Id).HasColumnName("CustomerID");
            builder.Property(p => p.Name).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Phone).HasMaxLength(12).IsRequired();
        }
    }
}