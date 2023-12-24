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
    public async Task<List<ChatDto>> GetChatList(Guid userId)
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
            .ToListAsync();
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
}