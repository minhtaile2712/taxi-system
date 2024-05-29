namespace TaxiSystem.Dtos.Customers;

public class CustomerDto
{
  public long Id { get; set; }

  public string PhoneNumber { get; set; } = null!;

  public LocationDto? Location { get; set; }
}
