using TaxiSystem.Models.Customers;

namespace TaxiSystem.Models.Bookings;

public class Booking
{
    public Customer Customer { get; set; }

    public Booking(Customer customer)
    {
        Customer = customer;
    }
}
