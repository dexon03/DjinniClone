using Core.Enums;
using Core.MessageContract;
using MassTransit;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;

namespace ProfilesService.Application.MessageConsumers;

public sealed class UserCreatedConsumer : IConsumer<UserCreatedEvent>
{
    private readonly IProfileService _profileService;

    public UserCreatedConsumer(IProfileService profileService)
    {
        _profileService = profileService;
    }
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var message = context.Message;
        var profile = new ProfileCreateDto
        {
            UserId = message.UserId,
            Name = message.FirstName,
            Surname = message.LastName,
            Email = message.Email,
            PhoneNumber = message.PhoneNumber,
            Role = Enum.Parse<Role>(message.Role)
        };
        
        await _profileService.CreateProfile(profile);
    }
}