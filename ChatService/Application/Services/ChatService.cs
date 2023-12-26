using ChatService.Domain.Contracts;
using ChatService.Domain.Dto;
using ChatService.Domain.Models;
using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Application.Services;

public class ChatService : IChatService
{
    private readonly IRepository _repository;

    public ChatService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<ChatDto>> GetChatList(Guid userId, CancellationToken cancellationToken = default)
    {
        var chats = await _repository.GetAll<Message>()
            .Include(m => m.Chat)
            .Where(m => m.ReceiverId == userId || m.SenderId == userId)
            .GroupBy(m => m.Chat)
            .Select(g => new Domain.Dto.ChatDto
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                SenderOfLastMessage = g.OrderByDescending(m => m.TimeStamp).First().Sender.UserName,
                LastMessage = g.OrderByDescending(m => m.TimeStamp).First().Content,
                IsLastMessageRead = g.OrderByDescending(m => m.TimeStamp).First().IsRead
            })
            .ToListAsync(cancellationToken);
        var temp = new List<ChatDto>
        {
            new ChatDto
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                SenderOfLastMessage = "Test",
                LastMessage = "Test",
                IsLastMessageRead = false
            },
            new ChatDto
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                SenderOfLastMessage = "Test",
                LastMessage = "Test",
                IsLastMessageRead = false
            },
            new ChatDto
            {
                Id = Guid.NewGuid(),
                Name = "Test2",
                SenderOfLastMessage = "Test",
                LastMessage = "Test",
                IsLastMessageRead = true
            },
        };
        return temp;
    }

    public async Task<List<MessageDto>> GetChatMessages(Guid chatId, CancellationToken cancellationToken = default)
    {
        var messages = await  _repository.GetAll<Message>()
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Where(m => m.ChatId == chatId)
            .Select(m => new MessageDto
            {
                Id = m.Id,
                ChatId = m.ChatId,
                Content = m.Content,
                TimeStamp = m.TimeStamp,
                Sender = m.Sender,
                Receiver = m.Receiver,
                IsRead = m.IsRead
            })
            .OrderBy(m => m.TimeStamp)
            .ToListAsync(cancellationToken);

        var temp = new List<MessageDto>()
        {
            new MessageDto
            {
                Id = Guid.NewGuid(),
                ChatId = Guid.NewGuid(),
                Content = "Test Message",
                TimeStamp = DateTime.Now,
                Sender = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "Test1"
                },
                Receiver = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "Test2"
                },
                IsRead = false
            }
        };
        return temp;
    }
}