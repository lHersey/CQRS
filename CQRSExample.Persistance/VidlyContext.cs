using System;
using System.Reflection;
using CQRSExample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRSExample.Persistance
{
    public class VidlyContext : DbContext
    {
        public VidlyContext(DbContextOptions<VidlyContext> options) : base(options){ }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VidlyContext).Assembly);
        }
    }
}
