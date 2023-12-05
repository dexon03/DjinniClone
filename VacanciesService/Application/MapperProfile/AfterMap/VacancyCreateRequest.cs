using AutoMapper;
using Core.Database;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.MapperProfile.AfterMap;

public class VacancyCreateRequest : IMappingAction<VacancyCreateDto, Vacancy>
{
    private readonly IRepository _repository;

    public VacancyCreateRequest(IRepository repository)
    {
        _repository = repository;
    }
    public void Process(VacancyCreateDto source, Vacancy destination, ResolutionContext context)
    {
        var vacancySkills = source.SkillIds?.Select(s => new VacancySkill
        {
            SkillId = s,
            VacancyId = destination.Id
        });
        var vacancyLocations = source.LocationIds?.Select(l => new LocationVacancy
        {
            LocationId = l,
            VacancyId = destination.Id
        });
        
        if (vacancySkills != null)
        {
            _repository.CreateRange(vacancySkills.ToArray());
        }
        if (vacancyLocations != null)
        {
            _repository.CreateRange(vacancyLocations.ToArray());
        }
    }
}