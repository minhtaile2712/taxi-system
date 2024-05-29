namespace TaxiSystem.Dtos;

public class DriverCreateDto
{
  public string PhoneNumber { get; set; } = null!;
  public string? Name { get; set; }
  public string? AvatarUrl { get; set; }
}
