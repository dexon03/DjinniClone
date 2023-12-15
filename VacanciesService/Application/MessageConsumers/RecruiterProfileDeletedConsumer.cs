using Core.Database;
using Core.MessageContract;
using MassTransit;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.MessageConsumers;

public class RecruiterProfileDeletedConsumer : IConsumer<RecruiterProfileDeletedEvent>
{
    private readonly IRepository _repository;

    public RecruiterProfileDeletedConsumer(IRepository repository)
    {
        _repository = repository;
    }
    public async Task Consume(ConsumeContext<RecruiterProfileDeletedEvent> context)
    {
        var message = context.Message;
        await _repository.DeleteRange<Vacancy>(v => v.RecruiterId == message.ProfileId);
        await _repository.SaveChangesAsync();
    }
}