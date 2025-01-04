using ChatService.Domain.Models;
using Core.Database;
using Core.MessageContract;
using MassTransit;

namespace ChatService.Application.MessageConsumers;

public class UserDeletedMessageConsumer(IRepository repository) : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var message = context.Message;
        await repository.DeleteRange<Message>(m => m.ReceiverId == message.UserId || m.SenderId == message.UserId);
        await repository.DeleteRange<User>(u => u.Id == message.UserId);
        await repository.SaveChangesAsync();
    }
}