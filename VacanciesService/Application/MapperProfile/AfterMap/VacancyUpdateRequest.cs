using AutoMapper;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.MapperProfile.AfterMap;

public class VacancyUpdateRequest : IMappingAction<VacancyUpdateDto, Vacancy>
{
    private readonly IRepository _repository;

    public VacancyUpdateRequest(IRepository repository)
    {
        _repository = repository;
    }
    public void Process(VacancyUpdateDto source, Vacancy destination, ResolutionContext context)
    {
        var vacancySkills = source.Skills?.Select(s => new VacancySkill
        {
            SkillId = s.Id,
            VacancyId = destination.Id
        });
        var vacancyLocations = source.Locations?.Select(l => new LocationVacancy
        {
            LocationId = l.Id,
            VacancyId = destination.Id
        });

        var existingEntities = _repository.GetAll<VacancySkill>()
            .Where(vs => vs.VacancyId == source.Id)
            .ToList();
        var existingLocations = _repository.GetAll<LocationVacancy>()
            .Where(lv => lv.VacancyId == source.Id)
            .ToList();
        
        
        if (vacancySkills != null)
        {
            var skillsToInsert = vacancySkills
                .Where(vs => existingEntities.All(ee => ee.SkillId != vs.SkillId));
            _repository.CreateRange(skillsToInsert.ToArray());
            var skillsToRemove = existingEntities
                .Where(ee => vacancySkills.All(vs => vs.SkillId != ee.SkillId));
            _repository.DeleteRange(skillsToRemove.ToArray());
        }
        if (vacancyLocations != null)
        {
            var locToInsert = vacancyLocations
                .Where(lv => existingLocations.All(el => el.LocationId != lv.LocationId));
            _repository.CreateRange(locToInsert.ToArray());
            var locToRemove = existingLocations
                .Where(el => vacancyLocations.All(lv => lv.LocationId != el.LocationId));
            _repository.DeleteRange(locToRemove.ToArray());
        }
    }
}