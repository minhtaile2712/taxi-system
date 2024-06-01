using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

    public void SetRadius(double radius)
    {
        Radius = radius;
    }

    public double GetRadius()
    {
        return Radius;
    }

    public async Task<BookingDto?> CreateBookingAsync(BookingCreateDto input)
    {
        var customer = await _context.Customers.FindAsync(input.CustomerId);
        if (customer == null || customer.Location == null) return null;

        var drivers = await _driversService.GetNearbyDriversAsync(customer.Location.X, customer.Location.Y, Radius);
        if (!drivers.Any()) return null;

        var booking = new Booking(customer, drivers);

        var driverIdToNotify = booking.NotifyNextDriver();
        await _hubContext.Clients.All.SendAsync("BookingCreatedToDriver", booking.Id, driverIdToNotify);

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        var result = MapBookingToDto(booking);
        return result;
    }

    public async Task AcceptBookingAsync(BookingAcceptDto input)
    {
        var driverId = input.DriverId;

        var booking = await _context.Bookings
            .Where(b => b.Id == input.BookingId)
            .Include(b => b.BookingDrivers)
            .FirstOrDefaultAsync();

        if (booking == null) return;
        if (!booking.BookingDrivers.Select(d => d.DriverId).Contains(driverId)) return;

        booking.Accept(driverId);
        await _context.SaveChangesAsync();

        await _hubContext.Clients.All.SendAsync("BookingAcceptedToCusTomer", booking.Id, booking.CustomerId, driverId);
    }

    public async Task DenyBookingAsync(BookingDenyDto input)
    {
        var driverId = input.DriverId;

        var booking = await _context.Bookings
            .Where(b => b.Id == input.BookingId)
            .Include(b => b.BookingDrivers)
            .FirstOrDefaultAsync();

        if (booking == null) return;
        if (!booking.BookingDrivers.Select(d => d.DriverId).Contains(driverId)) return;

        var driverIdToNotify = booking.Deny(driverId);
        await _context.SaveChangesAsync();

        if (driverIdToNotify == null)
            await _hubContext.Clients.All.SendAsync("BookingDeniedToCustomer", booking.Id, booking.CustomerId);
        else
            await _hubContext.Clients.All.SendAsync("BookingCreatedToDriver", booking.Id, driverIdToNotify);
    }

    public async Task CompleteBookingAsync(long id)
    {
        var booking = await _context.Bookings
            .Where(b => b.Id == id)
            .Include(b => b.BookingDrivers)
            .FirstOrDefaultAsync();

        if (booking == null) return;

        booking.Complete();
        await _context.SaveChangesAsync();

        await _hubContext.Clients.All.SendAsync("BookingCompleted", booking.Id);
    }

    public async Task CancelBookingAsync(long id)
    {
        var booking = await _context.Bookings
            .Where(b => b.Id == id)
            .Include(b => b.BookingDrivers)
            .FirstOrDefaultAsync();

        if (booking == null) return;

        booking.Cancel();
        await _context.SaveChangesAsync();

        await _hubContext.Clients.All.SendAsync("BookingCancelled", booking.Id);
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
