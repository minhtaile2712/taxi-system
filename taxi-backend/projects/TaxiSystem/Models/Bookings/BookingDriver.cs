using TaxiSystem.Models.Drivers;

namespace TaxiSystem.Models.Bookings;

public class BookingDriver
{
    public long Id { get; set; }

    public long BookingId { get; set; }

    public Driver Driver { get; set; } = null!;
    public long DriverId { get; set; }

    public BookingDriverState State { get; set; }
    public int PositionInQueue { get; set; }
}
