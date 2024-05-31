namespace TaxiSystem.Models.Bookings;

public class BookingDriver
{
    public long BookingId { get; set; }
    public long DriverId { get; set; }

    public BookingDriverState State { get; set; }
    public int PositionInQueue { get; set; }
}
