using ChatService.Domain.Contracts;
using ChatService.Domain.Dto;
using ChatService.Domain.Models;
using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Application.Services;

public class ChatService : IChatService
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public ChatService(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<List<ChatDto>> GetChatList(Guid userId, CancellationToken cancellationToken = default)
    {
        var chats = await _repository.GetAll<Message>()
            .Include(m => m.Chat)
            .AsSplitQuery()
            .Where(m => m.ReceiverId == userId || m.SenderId == userId)
            .GroupBy(m => m.Chat)
            .Select(g => new ChatDto
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                SenderOfLastMessage = g.OrderByDescending(m => m.TimeStamp).First().Sender.UserName,
                LastMessage = g.OrderByDescending(m => m.TimeStamp).First().Content,
                IsLastMessageRead = g.OrderByDescending(m => m.TimeStamp).First().IsRead
            })
            .ToListAsync(cancellationToken);
        return chats;
    }

    public async Task<List<MessageDto>> GetChatMessages(Guid chatId, CancellationToken cancellationToken = default)
    {
        var messages = await _repository.GetAll<Message>()
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .AsSplitQuery()
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
        return messages;
    }

    public async Task CreateChat(CreateChatDto chatDto, CancellationToken cancellationToken = default)
    {
        var existentChat = await _repository
            .FirstOrDefaultAsync<Message>(m => (m.ReceiverId == chatDto.ReceiverId && m.SenderId == chatDto.SenderId) 
                                    || (m.ReceiverId == chatDto.SenderId && m.SenderId == chatDto.ReceiverId));
        if (existentChat is not null)
        {
            var newMessage = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = existentChat.ChatId,
                Content = chatDto.Message,
                TimeStamp = DateTime.Now,
                SenderId = chatDto.SenderId,
                ReceiverId = chatDto.ReceiverId,
            };
            await _repository.CreateAsync(newMessage);
            await _repository.SaveChangesAsync(cancellationToken);
        }
        else
        {
           await CreateNewChatWithMessage(chatDto, cancellationToken);
        }
    }

    private async Task CreateNewChatWithMessage(CreateChatDto chatDto, CancellationToken cancellationToken = default)
    {
        await _userService.AddUsersIfNotExists(chatDto, cancellationToken);
        var chat = new Chat
        {
            Id = Guid.NewGuid(),
            Name = chatDto.SenderName + " / " + chatDto.ReceiverName,
        };
            
        var message = new Message
        {
            Id = Guid.NewGuid(),
            ChatId = chat.Id,
            Content = chatDto.Message,
            TimeStamp = DateTime.Now,
            SenderId = chatDto.SenderId,
            ReceiverId = chatDto.ReceiverId,
        };
            
        await _repository.CreateAsync(chat);
        await _repository.CreateAsync(message);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}