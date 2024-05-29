using Microsoft.EntityFrameworkCore;

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

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Driver>().ToTable("Drivers");
    //}
}
