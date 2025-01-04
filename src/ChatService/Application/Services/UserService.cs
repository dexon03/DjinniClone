using ChatService.Domain.Contracts;
using ChatService.Domain.Dto;
using ChatService.Domain.Models;
using Core.Database;

namespace ChatService.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;

    public UserService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task CreateUsersIfNotExists(CreateChatDto chatDto, CancellationToken cancellationToken)
    {
        var isSenderExists =  await _repository.AnyAsync<User>(u => u.Id == chatDto.SenderId);
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
}