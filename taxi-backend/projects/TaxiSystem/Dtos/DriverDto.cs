namespace TaxiSystem.Dtos;

public class DriverDto
{
    public long Id { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public LocationDto? Location { get; set; }
}
