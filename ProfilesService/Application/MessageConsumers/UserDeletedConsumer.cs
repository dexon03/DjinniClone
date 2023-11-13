using Core.MessageContract;
using MassTransit;
using ProfilesService.Domain.Contracts;

namespace ProfilesService.Application.MessageConsumers;

public class UserDeletedConsumer : IConsumer<UserDeletedEvent>
{
    private readonly IProfileService _profileService;

    public UserDeletedConsumer(IProfileService profileService)
    {
        _profileService = profileService;
    }
    public Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var message = context.Message;
        return _profileService.DeleteProfileByUserId(message.UserId);
    }
}