using Microsoft.EntityFrameworkCore;
using TaxiServer.Models.Customers;
using TaxiServer.Models.Drivers;

namespace TaxiServer.Models;

public class TaxiServerDbContext : DbContext
{
    public TaxiServerDbContext(DbContextOptions<TaxiServerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>().ToTable("Drivers");
        modelBuilder.Entity<Customer>().ToTable("Customers");
    }
}
