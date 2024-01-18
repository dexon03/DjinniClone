using ChatService.Domain.Contracts;
using ChatService.Domain.Dto;
using ChatService.Domain.Models;
using Core.Database;

namespace ChatService.Application.Services;

public class UserServices : IUserService
{
    private readonly IRepository _repository;

    public UserServices(IRepository repository)
    {
        _repository = repository;
    }
    public async Task CreateUsersIfNotExists(CreateChatDto chatDto, CancellationToken cancellationToken)
    {
        var isSenderExists =  _repository.AnyAsync<User>(u => u.Id == chatDto.SenderId);
        var isReceiverExists = _repository.AnyAsync<User>(u => u.Id == chatDto.ReceiverId);
        var result =  await Task.WhenAll(isSenderExists, isReceiverExists);
        if (!result[0])
        {
            var newSender = new User
            {
                Id = chatDto.SenderId,
                UserName = chatDto.SenderName
            };
            await _repository.CreateAsync(newSender);
        }
        if (!result[1])
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