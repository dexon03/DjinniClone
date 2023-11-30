using Core.MessageContract;
using MassTransit;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;

namespace VacanciesService.Application.MessageConsumers;

public class CompanyCreatedConsumer : IConsumer<CompanyCreatedEvent>
{
    private readonly ICompanyService _companyService;

    public CompanyCreatedConsumer(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task Consume(ConsumeContext<CompanyCreatedEvent> context)
    {
        var message = context.Message;
        await _companyService.CreateCompany(new CompanyCreateDto
        {
            Id = message.Id,
            Name = message.Name,
            Description = message.Description
        });
    }
}