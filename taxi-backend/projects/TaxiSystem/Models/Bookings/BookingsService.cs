using TaxiSystem.Dtos.Bookings;

namespace TaxiSystem.Models.Bookings;

public class BookingsService : IBookingsService
{
    public async Task<BookingDto> MakeABookingAsync(BookingMakeDto input)
    {
        var result = new BookingDto();
        return await Task.FromResult(result);
    }
}
