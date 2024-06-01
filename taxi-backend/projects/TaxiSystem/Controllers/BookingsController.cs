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
    /// Get booking by customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet("by-customer/{customerId}")]
    public async Task<BookingDto?> GetBookingByCustomer([FromRoute] long customerId)
    {
        var result = await _bookingsService.GetBookingByCustomerAsync(customerId);
        return result;
    }

    /// <summary>
    /// Get booking by driver
    /// </summary>
    /// <param name="driverId"></param>
    /// <returns></returns>
    [HttpGet("by-driver/{driverId}")]
    public async Task<BookingDto?> GetBookingByDriver([FromRoute] long driverId)
    {
        var result = await _bookingsService.GetBookingByDriverAsync(driverId);
        return result;
    }

    /// <summary>
    /// Accept a booking
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("accept")]
    public async Task AcceptBooking([FromBody] BookingAcceptDto input)
    {
        await _bookingsService.AcceptBookingAsync(input);
    }

    /// <summary>
    /// Deny a booking
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("deny")]
    public async Task DenyBooking([FromBody] BookingDenyDto input)
    {
        await _bookingsService.DenyBookingAsync(input);
    }

    /// <summary>
    /// Complete a booking
    /// </summary>
    /// <param name="bookingId">Booking Id</param>
    /// <returns></returns>
    [HttpPost("{bookingId}/complete")]
    public async Task CompleteBooking([FromRoute] long bookingId)
    {
        await _bookingsService.CompleteBookingAsync(bookingId);
    }

    /// <summary>
    /// Cancel a booking
    /// </summary>
    /// <param name="bookingId">Booking Id</param>
    /// <returns></returns>
    [HttpPost("{bookingId}/cancel")]
    public async Task CancelBooking([FromRoute] long bookingId)
    {
        await _bookingsService.CancelBookingAsync(bookingId);
    }
}
