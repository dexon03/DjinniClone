using ChatService.Domain.Dto;
using ChatService.Domain.Models;
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

    public Task JoinChatGroup(string chatId)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }

    public Task LeaveChatGroup(string chatId)
    {
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }

    public async Task SendMessage(SendMessageDto messageDto)
    {
        var messageEntity = new Message
        {
            Content = messageDto.Content,
            SenderId = messageDto.SenderId,
            ReceiverId = messageDto.ReceiverId,
            ChatId = messageDto.ChatId,
            TimeStamp = DateTime.Now,
        };
        await _repository.CreateAsync(messageEntity);
        await _repository.SaveChangesAsync();
        
        var message = new MessageDto
        {
            Id = messageEntity.Id,
            Content = messageDto.Content,
            Sender = new User
            {
                Id = messageDto.SenderId,
                UserName = messageDto.SenderName
            },
            
            ChatId = messageDto.ChatId,
            TimeStamp = DateTime.Now,
        };
        
        await Clients.Group(messageDto.ChatId.ToString()).SendAsync("ReceiveMessage", message);
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ConnectedUser", $"{Context.User.Identity.Name} joined the chat");
    }
}