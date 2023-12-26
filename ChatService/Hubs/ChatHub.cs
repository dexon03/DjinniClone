using Core.Database;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Hubs;

public class ChatHub : Hub
{
    private readonly IRepository _repository;
    public ChatHub(IRepository repository)
    {
        _repository = repository;
    }
    public override Task OnConnectedAsync()
    {
        return Clients.All.SendAsync("ReceiveMessage", $"{Context.User} joined the chat");
    }
    
    public async Task JoinGroup(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }

    public async Task LeaveGroup(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }
    
    public async Task ReceiveMessage(Guid senderId, Guid receiverId, string message)
    {
        
    }
    public async Task SendMessage(Guid senderId, Guid receiverId, string message)
    {
        
    }
}