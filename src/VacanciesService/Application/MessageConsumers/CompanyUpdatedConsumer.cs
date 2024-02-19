using Core.MessageContract;
using MassTransit;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;

namespace VacanciesService.Application.MessageConsumers;

public class CompanyUpdatedConsumer : IConsumer<CompanyUpdatedEvent>
{
    private readonly ICompanyService _companyService;

    public CompanyUpdatedConsumer(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task Consume(ConsumeContext<CompanyUpdatedEvent> context)
    {
        var message = context.Message;
        await _companyService.UpdateCompany(new CompanyUpdateDto
        {
            Id = message.Id,
            Name = message.Name,
            Description = message.Description
        });
    }
}