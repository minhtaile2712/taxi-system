using TaxiSystem.Dtos.Bookings;

namespace TaxiSystem.Models.Bookings;

public interface IBookingsService
{
    Task<BookingDto?> MakeABookingAsync(BookingMakeDto input);
}
