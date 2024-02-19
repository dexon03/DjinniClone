using ChatService.Domain.Dto;

namespace ChatService.Domain.Contracts;

public interface IUserService
{
    Task CreateUsersIfNotExists(CreateChatDto chatDto, CancellationToken cancellationToken);
}