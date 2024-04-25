using ChatService.Domain.Dto;

namespace ChatService.Domain.Contracts;

public interface IUserService
{
    Task AddUsersIfNotExists(CreateChatDto chatDto, CancellationToken cancellationToken);
}