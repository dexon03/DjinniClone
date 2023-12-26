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
            .Select(g => new ChatDto
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
        return chats;
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
                IsRead = false
            };
            await _repository.CreateAsync(newMessage);
            await _repository.SaveChangesAsync(cancellationToken);
        }
        else
        {
           await CreateNewChat(chatDto, cancellationToken);
        }
    }

    private async Task CreateUsersIfNotExists(CreateChatDto chatDto, CancellationToken cancellationToken = default)
    {
        var isSenderExists = await _repository.AnyAsync<User>(u => u.Id == chatDto.SenderId);
        var isReceiverExists = await _repository.AnyAsync<User>(u => u.Id == chatDto.ReceiverId);
        if (!isSenderExists)
        {
            var newSender = new User
            {
                Id = chatDto.SenderId,
                UserName = chatDto.SenderName
            };
            await _repository.CreateAsync(newSender);
        }
        if (!isReceiverExists)
        {
            var newReceiver = new User
            {
                Id = chatDto.ReceiverId,
                UserName = chatDto.ReceiverName
            };
            await _repository.CreateAsync(newReceiver);
        }
    }
    
    private async Task CreateNewChat(CreateChatDto chatDto, CancellationToken cancellationToken = default)
    {
        await CreateUsersIfNotExists(chatDto, cancellationToken);
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
            IsRead = false
        };
            
        await _repository.CreateAsync(chat);
        await _repository.CreateAsync(message);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}