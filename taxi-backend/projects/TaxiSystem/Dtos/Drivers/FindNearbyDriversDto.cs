using System.ComponentModel.DataAnnotations;

namespace TaxiSystem.Dtos.Drivers;

public class FindNearbyDriversDto
{
  [Required]
  public double Long { get; set; }

  [Required]
  public double Lat { get; set; }

  [Required]
  public double DistanceInMeters { get; set; }
}
