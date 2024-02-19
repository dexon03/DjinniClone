using ChatService.Domain.Models;
using Core.Database;
using Core.MessageContract;
using MassTransit;

namespace ChatService.Application.MessageConsumers;

public class UserDeletedMessageConsumer : IConsumer<UserDeletedEvent>
{
    private readonly IRepository _repository;

    public UserDeletedMessageConsumer(IRepository repository)
    {
        _repository = repository;
    }
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var message = context.Message;
        await _repository.DeleteRange<Message>(m => m.ReceiverId == message.UserId || m.SenderId == message.UserId);
        await _repository.DeleteRange<User>(u => u.Id == message.UserId);
        await _repository.SaveChangesAsync();
    }
}