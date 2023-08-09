using AutoMapper;
using MediatR;
using VacanciesService.Domain.Contracts;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Update;

public class UpdateVacancyQueryHandler : IRequestHandler<UpdateVacancyQuery,Vacancy>
{
    private readonly IVacancyRepository _repository;
    private readonly IMapper _mapper;

    public UpdateVacancyQueryHandler(IVacancyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Vacancy> Handle(UpdateVacancyQuery request, CancellationToken cancellationToken)
    {
        var vacancy = await _repository.Get(request.vacancy.Id);
        if (vacancy == null)
        {
            throw new Exception("Vacancy not found");
        }
        
        _mapper.Map(request.vacancy, vacancy);
        var result  = await _repository.Update(vacancy);
        return result;
    }
}