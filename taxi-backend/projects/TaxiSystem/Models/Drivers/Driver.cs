using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiSystem.Models.Drivers;

public class Driver
{
  public long Id { get; set; }

  public string PhoneNumber { get; set; }

  public string? Name { get; set; }

  public string? AvatarUrl { get; set; }

  public bool IsActive { get; set; }

  [Column(TypeName = "geometry (point)")]
  public Point? Location { get; set; }

  public Driver(string phoneNumber, string? name, string? avatarUrl)
  {
    PhoneNumber = phoneNumber;
    Name = name;
    AvatarUrl = avatarUrl;
  }

  public void SetLocation(Point location)
  {
    Location = location;
  }
}
