using AutoMapper;
using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Update;

public class UpdateVacancyQueryHandler : IRequestHandler<UpdateVacancyQuery,Vacancy>
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public UpdateVacancyQueryHandler(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Vacancy> Handle(UpdateVacancyQuery request, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.GetByIdAsync<Vacancy>(request.vacancy.Id);
        if (vacancy == null)
        {
            throw new Exception("Vacancy not found");
        }
        
        _mapper.Map(request.vacancy, vacancy);
        var result  = _repository.Update(vacancy);
        return result;
    }
}