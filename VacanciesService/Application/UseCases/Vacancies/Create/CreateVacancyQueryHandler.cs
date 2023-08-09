using AutoMapper;
using MediatR;
using VacanciesService.Domain.Contracts;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Create;

public class CreateVacancyQueryHandler : IRequestHandler<CreateVacancyQuery,Vacancy>
{
    private readonly IVacancyRepository _repository;
    private readonly IMapper _mapper;

    public CreateVacancyQueryHandler(IVacancyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Vacancy> Handle(CreateVacancyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Create(request.vacancy);
        return result;
    }
}