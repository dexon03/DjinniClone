using Core.Database;
using Core.Enums;
using Core.MessageContract;
using MassTransit;
using ProfilesService.Domain.Models;

namespace ProfilesService.Application.MessageConsumers;

public class UserUpdatedConsumer : IConsumer<UserUpdatedEvent>
{
    private readonly IRepository _repository;

    public UserUpdatedConsumer(IRepository repository)
    {
        _repository = repository;
    }
    public Task Consume(ConsumeContext<UserUpdatedEvent> context)
    {
        var message = context.Message;
        if (message.Role == Role.Recruiter)
        {
            return UpdateRecruiterProfile(message);
        }
        else if (message.Role == Role.Candidate)
        {
            return UpdateCandidateProfile(message);
        }

        return Task.CompletedTask;
    }
    
    private async Task UpdateCandidateProfile(UserUpdatedEvent message)
    {
        var profile = await _repository.FirstOrDefaultAsync<CandidateProfile>(x => x.UserId == message.Id);
        if (profile is null)
        {
            throw new Exception("Profile not found");
        }

        profile.Name = message.FirstName;
        profile.Surname = message.LastName;
        profile.Email = message.Email;
        profile.PhoneNumber = message.PhoneNumber;
        _repository.Update(profile);
        await _repository.SaveChangesAsync();
    }
    
    private async Task UpdateRecruiterProfile(UserUpdatedEvent message)
    {
        var profile = await _repository.FirstOrDefaultAsync<RecruiterProfile>(x => x.UserId == message.Id);
        if (profile is null)
        {
            throw new Exception("Profile not found");
        }
        
        profile.Name = message.FirstName;
        profile.Surname = message.LastName;
        profile.Email = message.Email;
        profile.PhoneNumber = message.PhoneNumber;
        _repository.Update(profile);
        await _repository.SaveChangesAsync();
    }
}