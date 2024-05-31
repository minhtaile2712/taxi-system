using TaxiSystem.Dtos.Bookings;

namespace TaxiSystem.Models.Bookings;

public interface IBookingsService
{
    public void SetDistance(double radius);
    Task<BookingDto?> CreateBookingAsync(BookingCreateDto input);
    Task AcceptBookingAsync(BookingAcceptDto input);
    Task DenyBookingAsync(BookingDenyDto input);
}
