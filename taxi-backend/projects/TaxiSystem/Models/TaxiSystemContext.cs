using Microsoft.EntityFrameworkCore;
using TaxiSystem.Models.Bookings;
using TaxiSystem.Models.Customers;
using TaxiSystem.Models.Drivers;

namespace TaxiSystem.Models;

public class TaxiSystemContext : DbContext
{
    //static TaxiSystemDbContext()
    //=> NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();

    public TaxiSystemContext(DbContextOptions<TaxiSystemContext> options)
        : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Driver>().ToTable("Drivers");
    //}
}
