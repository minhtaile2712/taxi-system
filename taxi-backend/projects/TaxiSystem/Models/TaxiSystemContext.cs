using Microsoft.EntityFrameworkCore;
using TaxiSystem.Models.Bookings;
using TaxiSystem.Models.Customers;
using TaxiSystem.Models.Drivers;

namespace TaxiSystem.Models;

public class TaxiSystemContext : DbContext
{
    public DbSet<Driver> Drivers { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;

    public TaxiSystemContext(DbContextOptions<TaxiSystemContext> options)
        : base(options)
    {
    }
}
