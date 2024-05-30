using TaxiSystem.Models.Customers;

namespace TaxiSystem.Models.Bookings;

public class Booking
{
    public long Id { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public BookingState State { get; set; }

    protected Booking() { }

    public Booking(Customer customer)
    {
        Customer = customer;
        State = BookingState.Booked;
    }
}
