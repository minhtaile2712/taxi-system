using TaxiSystem.Dtos.Bookings;

namespace TaxiSystem.Models.Bookings;

public interface IBookingsService
{
    public void SetDistance(double radius);
    Task<BookingDto?> MakeABookingAsync(BookingMakeDto input);
    Task<BookingDto?> CancelBookingByIdAsync(long id);
}
