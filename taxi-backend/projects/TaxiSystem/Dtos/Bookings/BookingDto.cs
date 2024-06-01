namespace TaxiSystem.Dtos.Bookings;

public class BookingDto
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public bool IsAccepted { get; set; }
}
