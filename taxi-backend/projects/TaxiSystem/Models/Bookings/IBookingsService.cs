using TaxiSystem.Dtos.Bookings;

namespace TaxiSystem.Models.Bookings;

public interface IBookingsService
{
    void SetRadius(double radius);
    double GetRadius();
    Task<BookingDto?> CreateBookingAsync(BookingCreateDto input);

    Task<BookingDto?> GetBookingByCustomerAsync(long customerId);
    Task<BookingDto?> GetBookingByDriverAsync(long driverId);
    Task AcceptBookingAsync(BookingAcceptDto input);
    Task DenyBookingAsync(BookingDenyDto input);
    Task CompleteBookingAsync(long id);
    Task CancelBookingAsync(long id);
}
