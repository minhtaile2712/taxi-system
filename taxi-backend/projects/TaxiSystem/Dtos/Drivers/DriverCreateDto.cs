using System.ComponentModel.DataAnnotations;

namespace TaxiSystem.Dtos.Drivers;

public class DriverCreateDto
{
  [Required]
  public string PhoneNumber { get; set; } = null!;
  public string? Name { get; set; }
  public string? AvatarUrl { get; set; }
}
