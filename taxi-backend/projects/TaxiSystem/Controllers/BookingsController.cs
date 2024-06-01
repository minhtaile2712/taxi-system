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
    /// Set booking radius
    /// </summary>
    /// <param name="radius"></param>
    [HttpPost("radius")]
    public void SetRadius([FromBody] double radius)
    {
        _bookingsService.SetRadius(radius);
    }

    /// <summary>
    /// Get booking radius
    /// </summary>
    /// <param name="radius"></param>
    [HttpGet("radius")]
    public double GetRadius()
    {
        return _bookingsService.GetRadius();
    }

    /// <summary>
    /// Create a booking
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult<BookingDto>> CreateBooking(BookingCreateDto input)
    {
        var result = await _bookingsService.CreateBookingAsync(input);
        return (result == null) ? NotFound() : CreatedAtAction(nameof(CreateBooking), new { id = result.Id }, result);
    }

    /// <summary>
    /// Accept a booking
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("accept")]
    public async Task AcceptBooking(BookingAcceptDto input)
    {
        await _bookingsService.AcceptBookingAsync(input);
    }

    /// <summary>
    /// Deny a booking
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("deny")]
    public async Task DenyBooking(BookingDenyDto input)
    {
        await _bookingsService.DenyBookingAsync(input);
    }

    /// <summary>
    /// Complete a booking
    /// </summary>
    /// <param name="id">Booking Id</param>
    /// <returns></returns>
    [HttpPost("complete")]
    public async Task CompleteBooking(long id)
    {
        await _bookingsService.CompleteBookingAsync(id);
    }

    /// <summary>
    /// Cancel a booking
    /// </summary>
    /// <param name="id">Booking Id</param>
    /// <returns></returns>
    [HttpPost("cancel")]
    public async Task CancelBooking(long id)
    {
        await _bookingsService.CancelBookingAsync(id);
    }
}
