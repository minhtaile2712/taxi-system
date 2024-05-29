using TaxiSystem.Models.Customers;

namespace TaxiSystem.Models.Bookings;

public class Booking
{
    public long Id { get; set; }

    public Customer Customer { get; set; }

    public Booking(Customer customer)
    {
        Customer = customer;
    }
}
