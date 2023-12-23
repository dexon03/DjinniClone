using Microsoft.AspNetCore.SignalR;

namespace ChatService.Hubs;

public class ChatHub : Hub
{
    public async Task ReceiveMessage(Guid senderId, Guid receiverId, string message)
    {
        await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", senderId, message);
    }
    public async Task SendMessage(Guid senderId, Guid receiverId, string message)
    {
        // Store the message in the database
        // Send the message to the receiver
        await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", senderId, message);
    }
}