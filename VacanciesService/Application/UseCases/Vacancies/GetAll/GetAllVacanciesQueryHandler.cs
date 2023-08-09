using MediatR;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Database;
using VacanciesService.Domain.Contracts;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.GetAll;

public class GetAllVacanciesQueryHandler : IRequestHandler<GetAllVacanciesQuery, List<Vacancy>>
{
    private readonly IVacancyRepository _repository;

    public GetAllVacanciesQueryHandler(IVacancyRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<Vacancy>> Handle(GetAllVacanciesQuery request, CancellationToken cancellationToken)
    {
        var vacancies = await _repository.GetAll();

        var result = vacancies.ToList();
        return result;
    }
}