using TaxiSystem.Models.Customers;
using TaxiSystem.Models.Drivers;

namespace TaxiSystem.Models.Bookings;

public class Booking
{
    public long Id { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; } = null!; // optional

    public List<BookingDriver> BookingDrivers { get; set; } = [];
    public BookingState State { get; set; }

    protected Booking() { }

    public Booking(Customer customer, List<Driver> drivers)
    {
        Customer = customer;
        State = BookingState.Booked;

        for (int i = 0; i < drivers.Count; i++)
            BookingDrivers.Add(new()
            {
                BookingId = Id,
                DriverId = drivers[i].Id,
                State = BookingDriverState.Queued,
                PositionInQueue = i
            });
    }

    public long? NotifyNextDriver(long? deniedDriverId = null)
    {
        if (deniedDriverId != null)
        {
            BookingDriver deniedDriver = BookingDrivers.First(d => d.DriverId == deniedDriverId);
            deniedDriver.State = BookingDriverState.Denied;
        }

        var driver = BookingDrivers
            .Where(d => d.State == BookingDriverState.Queued)
            .OrderBy(d => d.PositionInQueue)
            .FirstOrDefault();

        if (driver == null) return null;

        driver.State = BookingDriverState.Notified;
        return driver.DriverId;
    }

    public void Accept(long driverId)
    {
        BookingDriver acceptedDriver = BookingDrivers.First(d => d.DriverId == driverId);
        acceptedDriver.State = BookingDriverState.Accepted;

        State = BookingState.Accepted;
    }

    public long? Deny(long? deniedDriverId)
    {
        var driverIdToNotify = NotifyNextDriver(deniedDriverId);
        if (driverIdToNotify == null)
            State = BookingState.Denied;
        return driverIdToNotify;
    }

    public void Complete()
    {
        State = BookingState.Completed;
        BookingDrivers.ForEach(d => d.State = BookingDriverState.Completed);
    }

    public void Cancel()
    {
        State = BookingState.Cancelled;
        BookingDrivers.ForEach(d => d.State = BookingDriverState.Completed);
    }
}
