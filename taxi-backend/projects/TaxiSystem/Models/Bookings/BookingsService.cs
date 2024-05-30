using TaxiSystem.Dtos.Bookings;
using TaxiSystem.Models.Drivers;

namespace TaxiSystem.Models.Bookings;

public class BookingsService : IBookingsService
{
    private readonly TaxiSystemContext _context;
    private readonly IDriversService _driversService;

    private static double Radius;

    public BookingsService(
        TaxiSystemContext context,
        IDriversService driversService)
    {
        _context = context;
        _driversService = driversService;
    }

    public void SetDistance(double radius)
    {
        Radius = radius;
    }

    public async Task<BookingDto?> MakeABookingAsync(BookingMakeDto input)
    {
        var customer = await _context.Customers.FindAsync(input.CustomerId);
        if (customer == null || customer.Location == null) return null;

        var booking = new Booking(customer);
        _context.Bookings.Add(booking);

        await _context.SaveChangesAsync();

        var result = MapBookingToDto(booking);
        return result;
    }

    private static BookingDto MapBookingToDto(Booking d)
    {
        var result = new BookingDto
        {
            Id = d.Id
        };
        return result;
    }
}
