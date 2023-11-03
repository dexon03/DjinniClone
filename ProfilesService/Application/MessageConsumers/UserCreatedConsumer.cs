using MassTransit;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;

namespace ProfilesService.Application.MessageConsumers;

public sealed class UserCreatedConsumer : IConsumer<UserCreatedConsumer>
{
    private readonly IProfileService _profileService;

    public UserCreatedConsumer(IProfileService profileService)
    {
        _profileService = profileService;
    }
    public async Task Consume(ConsumeContext<UserCreatedConsumer> context)
    {
       
    }
}