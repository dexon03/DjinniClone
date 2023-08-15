using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Get;

public class GetVacancyQueryHandler : IRequestHandler<GetVacancyQuery, Vacancy>
{
    private readonly IRepository _repository;

    public GetVacancyQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Vacancy> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.GetByIdAsync<Vacancy>(request.Id);
        if (vacancy == null)
        {
            throw new Exception("Vacancy not found");
        }
        
        return vacancy;
    }
}