using Microsoft.AspNetCore.SignalR;
using TaxiSystem.Dtos;
using TaxiSystem.Models.Bookings;
using TaxiSystem.Models.Customers;
using TaxiSystem.Models.Drivers;

namespace TaxiSystem.Hubs;

public class Message
{
    public string Content { get; set; } = null!;
    public string SenderId { get; set; } = null!;
    public DateTimeOffset SentTime { get; set; }
}

public class TaxiHub : Hub
{
    private readonly IDriversService _driversService;
    private readonly ICustomersService _customersService;
    private readonly IBookingsService _bookingsService;


    private static readonly List<Message> MessageHistory = new();

    public TaxiHub(
        IDriversService driversService,
        ICustomersService customersService,
        IBookingsService bookingsService)
    {
        _driversService = driversService;
        _customersService = customersService;
        _bookingsService = bookingsService;
    }

    public async Task SendMessageToOthers(string content)
    {
        var senderId = Context.ConnectionId;
        DateTimeOffset sentTime = WriteHisory(content, senderId);
        await Clients.Others.SendAsync("ReceiveMessage", content, senderId, sentTime);
    }

    public async Task SendMessageToAll(string content)
    {
        var senderId = Context.ConnectionId;
        DateTimeOffset sentTime = WriteHisory(content, senderId);
        await Clients.All.SendAsync("ReceiveMessage", content, senderId, sentTime);
    }

    public async Task RetrieveMessageHistory() =>
        await Clients.Caller.SendAsync("MessageHistory", MessageHistory);

    private DateTimeOffset WriteHisory(string content, string senderId)
    {
        var sentTime = DateTimeOffset.UtcNow;
        MessageHistory.Add(new()
        {
            Content = content,
            SenderId = senderId,
            SentTime = sentTime
        });
        return sentTime;
    }

    public async Task UpdateDriverLocation(long driverId, double longitude, double latitude)
    {
        await _driversService.UpdateDriverLocationAsync(driverId, new LocationDto { Long = longitude, Lat = latitude });
    }

    public async Task UpdateCustomerLocation(long customerId, double longitude, double latitude)
    {
        await _customersService.UpdateCustomerLocationAsync(customerId, new LocationDto { Long = longitude, Lat = latitude });
    }
}
