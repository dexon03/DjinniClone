using ChatService.Domain.Dto;
using ChatService.Domain.Models;

namespace ChatService.Domain.Contracts;

public interface IChatService
{
    Task<List<ChatDto>> GetChatList(Guid userId, CancellationToken cancellationToken = default);
    Task<List<MessageDto>> GetChatMessages(Guid chatId, CancellationToken cancellationToken = default);
}