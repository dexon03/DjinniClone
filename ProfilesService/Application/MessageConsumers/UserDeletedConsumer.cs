using Core.MessageContract;
using MassTransit;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.Models;

namespace ProfilesService.Application.MessageConsumers;

public class UserDeletedConsumer : IConsumer<UserDeletedEvent>
{
    private readonly IProfileService _profileService;

    public UserDeletedConsumer(IProfileService profileService)
    {
        _profileService = profileService;
    }
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var message = context.Message;
        await _profileService.DeleteProfileByUserId<CandidateProfile>(message.UserId);
        await _profileService.DeleteProfileByUserId<RecruiterProfile>(message.UserId);
    }
}