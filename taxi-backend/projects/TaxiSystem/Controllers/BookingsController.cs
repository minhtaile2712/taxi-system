using Microsoft.AspNetCore.Mvc;
using TaxiSystem.Dtos.Bookings;
using TaxiSystem.Models.Bookings;

namespace TaxiSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly IBookingsService _bookingsService;

    public BookingsController(IBookingsService bookingsService)
    {
        _bookingsService = bookingsService;
    }

    /// <summary>
    /// Make a booking
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<BookingDto>> MakeABooking(BookingMakeDto input)
    {
        var result = await _bookingsService.MakeABookingAsync(input);
        return (result == null) ? NotFound() : CreatedAtAction(nameof(MakeABooking), new { id = result.Id }, result);
    }
}
