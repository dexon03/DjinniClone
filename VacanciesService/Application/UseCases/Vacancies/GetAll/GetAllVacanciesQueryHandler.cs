using Core.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.GetAll;

public class GetAllVacanciesQueryHandler : IRequestHandler<GetAllVacanciesQuery, List<Vacancy>>
{
    private readonly IRepository _repository;

    public GetAllVacanciesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<Vacancy>> Handle(GetAllVacanciesQuery request, CancellationToken cancellationToken)
    {
        var vacancies = _repository.GetAll<Vacancy>();

        var result = await vacancies.ToListAsync(cancellationToken);
        return result;
    }
}