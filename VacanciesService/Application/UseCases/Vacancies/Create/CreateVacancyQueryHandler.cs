using AutoMapper;
using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Create;

public class CreateVacancyQueryHandler : IRequestHandler<CreateVacancyQuery,Vacancy>
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public CreateVacancyQueryHandler(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Vacancy> Handle(CreateVacancyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.CreateAsync(request.vacancy);
        return result;
    }
}