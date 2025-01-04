using ChatService.Domain.Dto;

namespace ChatService.Domain.Contracts;

public interface IChatService
{
    Task<List<ChatDto>> GetChatList(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<List<MessageDto>> GetChatMessages(Guid chatId, CancellationToken cancellationToken = default);
    Task CreateChat(CreateChatDto chatDto, CancellationToken cancellationToken = default);
}