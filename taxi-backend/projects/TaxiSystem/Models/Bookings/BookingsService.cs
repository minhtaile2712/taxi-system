using TaxiSystem.Dtos.Bookings;

namespace TaxiSystem.Models.Bookings;

public class BookingsService : IBookingsService
{
    private readonly TaxiSystemContext _context;

    public BookingsService(TaxiSystemContext context)
    {
        _context = context;
    }

    public async Task<BookingDto?> MakeABookingAsync(BookingMakeDto input)
    {
        var customer = await _context.Customers.FindAsync(input.CustomerId);
        if (customer == null) return null;


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
