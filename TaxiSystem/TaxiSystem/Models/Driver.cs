using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiSystem.Models;

public class Driver
{
    public long Id { get; set; }

    public string PhoneNumber { get; set; }

    [Column(TypeName = "geometry (point)")]
    public Point? Location { get; set; }

    public bool IsActive { get; set; }

    public Driver(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void SetLocation(Point location)
    {
        Location = location;
    }
}
