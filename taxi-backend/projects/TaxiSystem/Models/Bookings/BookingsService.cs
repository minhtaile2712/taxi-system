using Microsoft.AspNetCore.SignalR;
using TaxiSystem.Dtos;
using TaxiSystem.Dtos.Bookings;
using TaxiSystem.Hubs;
using TaxiSystem.Models.Drivers;

namespace TaxiSystem.Models.Bookings;

public class BookingsService : IBookingsService
{
    private readonly TaxiSystemContext _context;
    private readonly IDriversService _driversService;
    private readonly IHubContext<TaxiHub> _hubContext;

    private static double Radius;

    public BookingsService(
        TaxiSystemContext context,
        IDriversService driversService,
        IHubContext<TaxiHub> hubContext)
    {
        _context = context;
        _driversService = driversService;
        _hubContext = hubContext;
    }

    public void SetDistance(double radius)
    {
        Radius = radius;
    }

    public async Task<BookingDto?> MakeABookingAsync(BookingMakeDto input)
    {
        var customer = await _context.Customers.FindAsync(input.CustomerId);
        if (customer == null || customer.Location == null) return null;

        var booking = new Booking(customer);
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        var longitude = customer.Location.X;
        var latitude = customer.Location.Y;
        var location = new LocationDto { Long = longitude, Lat = latitude };

        var drivers = await _driversService.GetNearbyDriversAsync(longitude, latitude, Radius);
        var driverIds = drivers.Select(d => d.Id).ToList();

        await _hubContext.Clients.All.SendAsync("NewBooking", booking.Id, driverIds, customer.Id, location);

        var result = MapBookingToDto(booking);
        return result;
    }

    public async Task<BookingDto?> CancelBookingByIdAsync(long id)
    {
        BookingDto? result = null;

        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            booking.State = BookingState.Cancelled;
            await _context.SaveChangesAsync();
            result = MapBookingToDto(booking);
        }

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
