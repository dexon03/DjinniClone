using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Core.Database;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.Services;

public class VacancyService : IVacanciesService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public VacancyService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<VacancyGetAllDto>> GetAllVacancies(CancellationToken cancellationToken = default)
    {
        var vacancies = await GetAllVacanciesByCondition(v => v.IsActive == true, cancellationToken);
        return vacancies;
    }

    public async Task<VacancyGetDto> GetVacancyById(Guid id, CancellationToken cancellationToken = default)
    {
        var vacancyEntity = (await
                (from vacancy in _repository.GetAll<Vacancy>().Where(v => v.Id == id)
                    join category in _repository.GetAll<Category>()
                        on vacancy.CategoryId equals category.Id
                    join company in _repository.GetAll<Company>()
                        on vacancy.CompanyId equals company.Id
                    join locationVacancy in _repository.GetAll<LocationVacancy>()
                        on vacancy.Id equals locationVacancy.VacancyId into locationVacancyGroup
                    from locationVacancy in locationVacancyGroup.DefaultIfEmpty()
                    join location in _repository.GetAll<Location>()
                        on locationVacancy.LocationId equals location.Id into locationGroup
                    from location in locationGroup.DefaultIfEmpty()
                    join vacancySkill in _repository.GetAll<VacancySkill>()
                        on vacancy.Id equals vacancySkill.VacancyId into vacancySkillGroup
                    from vacancySkill in vacancySkillGroup.DefaultIfEmpty()
                    join skill in _repository.GetAll<Skill>()
                        on vacancySkill.SkillId equals skill.Id into skillGroup
                    from skill in skillGroup.DefaultIfEmpty()
                    select new
                    {
                        VacancyId = vacancy.Id,
                        vacancy.RecruiterId,
                        vacancy.Title,
                        vacancy.PositionTitle,
                        vacancy.Salary,
                        vacancy.CreatedAt,
                        vacancy.Experience,
                        vacancy.AttendanceMode,
                        vacancy.Description,
                        vacancy.IsActive,
                        CategoryId = category.Id,
                        CategoryName = category.Name,
                        CompanyId = company.Id,
                        CompanyName = company.Name,
                        CompanyDescription = company.Description,
                        LocationId = location == null ? Guid.Empty : location.Id,
                        LocationCity = location == null ? String.Empty : location.City,
                        LocationCountry = location == null ? String.Empty : location.Country,
                        SkillId = skill == null ? Guid.Empty : skill.Id,
                        SkillName = skill == null ? String.Empty : skill.Name
                    }).ToListAsync(cancellationToken)
            ).GroupBy(v => new
            {
                v.VacancyId,
                v.RecruiterId,
                v.Title,
                v.PositionTitle,
                v.Salary,
                v.Experience,
                v.CreatedAt,
                v.AttendanceMode,
                v.Description,
                v.IsActive,
                v.CategoryId,
                v.CategoryName,
                v.CompanyId,
                v.CompanyName,
                v.CompanyDescription
            }).Select(gv => new VacancyGetDto
            {
                Id = gv.Key.VacancyId,
                RecruiterId = gv.Key.RecruiterId,
                Title = gv.Key.Title,
                PositionTitle = gv.Key.PositionTitle,
                Salary = gv.Key.Salary,
                Experience = gv.Key.Experience,
                CreatedAt = gv.Key.CreatedAt,
                AttendanceMode = gv.Key.AttendanceMode,
                Description = gv.Key.Description,
                IsActive = gv.Key.IsActive,
                Category = new Category
                {
                    Id = gv.Key.CategoryId,
                    Name = gv.Key.CategoryName
                },
                Company = new Company
                {
                    Id = gv.Key.CompanyId,
                    Name = gv.Key.CompanyName,
                    Description = gv.Key.CompanyDescription
                },
                Locations = gv.All(l => l.LocationId != Guid.Empty)
                    ? gv.GroupBy(l => new
                    {
                        l.LocationId,
                        l.LocationCity,
                        l.LocationCountry
                    }).Select(gl => new Location
                    {
                        Id = gl.Key.LocationId,
                        City = gl.Key.LocationCity,
                        Country = gl.Key.LocationCountry
                    })
                    : null,
                Skills = gv.All(s => s.SkillId != Guid.Empty)
                    ? gv.GroupBy(s => new
                    {
                        s.SkillId,
                        s.SkillName
                    }).Select(gs => new Skill
                    {
                        Id = gs.Key.SkillId,
                        Name = gs.Key.SkillName
                    })
                    : null
            }).FirstOrDefault();
        
        if (vacancyEntity == null)
        {
            throw new ExceptionWithStatusCode("Vacancy not found", HttpStatusCode.BadRequest);
        }

        return vacancyEntity;
    }

    public async Task<List<VacancyGetAllDto>> GetVacanciesByRecruiterId(Guid recruiterId, CancellationToken cancellationToken = default)
    {
        var vacancies = await GetAllVacanciesByCondition(v => v.RecruiterId == recruiterId, cancellationToken);
        return vacancies;
    }

    public async Task<Vacancy> CreateVacancy(VacancyCreateDto vacancyDto, CancellationToken cancellationToken = default)
    {
        var vacancy = new Vacancy
        {
            Id = Guid.NewGuid()
        };
        var vacancyEntity = _mapper.Map(vacancyDto, vacancy);
        vacancyEntity.CreatedAt = DateTime.Now;
        var result = await _repository.CreateAsync(vacancyEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;

    }

    public async Task<Vacancy> UpdateVacancy(VacancyUpdateDto vacancy, CancellationToken cancellationToken = default)
    {
        var vacancyEntity = _mapper.Map<Vacancy>(vacancy);
        var isExists = await _repository.AnyAsync<Vacancy>(x => x.Id == vacancyEntity.Id);
        if (!isExists)
        {
            throw new ExceptionWithStatusCode("Vacancy that you trying to update, not exist", HttpStatusCode.BadRequest);
        }
        vacancyEntity.UpdatedAt = DateTime.Now;
        var result = _repository.Update(vacancyEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task DeleteVacancy(Guid id, CancellationToken cancellationToken = default)
    {
        var vacancy = await _repository.GetByIdAsync<Vacancy>(id);
        if (vacancy == null)
        {
            throw new ExceptionWithStatusCode("Vacancy not found", HttpStatusCode.BadRequest);
        }
        _repository.Delete(vacancy);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task ActivateDeactivateVacancy(Guid id, CancellationToken cancellationToken = default)
    {
        var vacancy = await _repository.GetByIdAsync<Vacancy>(id);
        if (vacancy == null)
        {
            throw new ExceptionWithStatusCode("Vacancy not found", HttpStatusCode.BadRequest);
        }
        vacancy.IsActive = !vacancy.IsActive;
        vacancy.UpdatedAt = DateTime.Now;
        _repository.Update(vacancy);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    private async Task<List<VacancyGetAllDto>> GetAllVacanciesByCondition(Expression<Func<Vacancy, bool>> predicate, CancellationToken cancellationToken = default)
    {
        // TODO: Refactor this query
        var vacancies = 
                await (from vacancy in _repository.GetAll<Vacancy>().Where(predicate)
                join locationVacancy in _repository.GetAll<LocationVacancy>() 
                    on vacancy.Id equals locationVacancy.VacancyId
                join location in _repository.GetAll<Location>()
                    on locationVacancy.LocationId equals location.Id
                join company in _repository.GetAll<Company>()
                    on vacancy.CompanyId equals company.Id
                select new
                {
                    vacancy.Id,
                    vacancy.Title,
                    vacancy.PositionTitle,
                    vacancy.Salary,
                    vacancy.CreatedAt,
                    vacancy.Experience,
                    vacancy.AttendanceMode,
                    vacancy.Description,
                    vacancy.IsActive,
                    company.Name,
                    location.City,
                    location.Country
                })
                .ToListAsync(cancellationToken);
        var groupedVacancies = vacancies
                .GroupBy(v => new
                {
                    v.Id,
                    v.Title,
                    v.PositionTitle,
                    v.Salary,
                    v.Experience,
                    v.CreatedAt,
                    v.AttendanceMode,
                    v.Description,
                    v.Name,
                    v.IsActive,
                }).Select(g => new VacancyGetAllDto
                {
                    Id = g.Key.Id,
                    Title = g.Key.Title,
                    PositionTitle = g.Key.PositionTitle,
                    Salary = g.Key.Salary,
                    Experience = g.Key.Experience.ToString(),
                    CreatedAt = g.Key.CreatedAt,
                    AttendanceMode = g.Key.AttendanceMode.ToString(),
                    Description = g.Key.Description,
                    CompanyName = g.Key.Name,
                    IsActive = g.Key.IsActive,
                    Locations = g.Select(l => new LocationGetDto
                    {
                        City = l.City,
                        Country = l.Country
                    })
                });
        return groupedVacancies.ToList();
    }
}