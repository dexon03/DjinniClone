using MediatR;
using VacanciesService.Database;
using VacanciesService.Domain.Contracts;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Get;

public class GetVacancyQueryHandler : IRequestHandler<GetVacancyQuery, Vacancy>
{
    private readonly IVacancyRepository _repository;

    public GetVacancyQueryHandler(IVacancyRepository repository)
    {
        _repository = repository;
    }
    public async Task<Vacancy> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.Get(request.Id);
        if (vacancy == null)
        {
            throw new Exception("Vacancy not found");
        }
        
        return vacancy;
    }
}