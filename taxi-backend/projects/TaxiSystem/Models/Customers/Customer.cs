using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiSystem.Models.Customers;

public class Customer
{
  public long Id { get; set; }

  public string PhoneNumber { get; set; }

  [Column(TypeName = "geometry (point)")]
  public Point? Location { get; set; }

  public Customer(string phoneNumber)
  {
    PhoneNumber = phoneNumber;
  }

  public void SetLocation(Point location)
  {
    Location = location;
  }
}
