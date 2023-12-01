using Core.MessageContract;
using MassTransit;
using VacanciesService.Domain.Contacts;

namespace VacanciesService.Application.MessageConsumers;

public class CompanyDeletedConsumer : IConsumer<CompanyDeletedEvent>
{
    private readonly ICompanyService _companyService;

    public CompanyDeletedConsumer(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task Consume(ConsumeContext<CompanyDeletedEvent> context)
    {
        var message = context.Message;
        await _companyService.DeleteCompany(message.Id);
    }
}