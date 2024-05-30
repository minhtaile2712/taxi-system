using Microsoft.AspNetCore.SignalR;

namespace TaxiSystem.Hubs;

public class Message
{
    public string Content { get; set; } = null!;
    public string SenderId { get; set; } = null!;
    public DateTimeOffset SentTime { get; set; }
}

public class MyHub : Hub
{
    private static readonly List<Message> MessageHistory = new();

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
}
