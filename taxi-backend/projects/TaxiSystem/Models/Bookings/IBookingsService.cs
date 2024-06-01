using TaxiSystem.Dtos.Bookings;

namespace TaxiSystem.Models.Bookings;

public interface IBookingsService
{
    void SetRadius(double radius);
    double GetRadius(double radius);
    Task<BookingDto?> CreateBookingAsync(BookingCreateDto input);
    Task AcceptBookingAsync(BookingAcceptDto input);
    Task DenyBookingAsync(BookingDenyDto input);
    Task CompleteBookingAsync(long id);
    Task CancelBookingAsync(long id);
}
