using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contracts;
using VacanciesService.Domain.Models;

namespace VacanciesService.Database.Repository;

public class VacancyRepository : IVacancyRepository
{
    private readonly VacanciesDbContext _context;

    public VacancyRepository(VacanciesDbContext context)
    {
        _context = context;
    }
    
    public async Task<Vacancy?> Get(Guid id)
    {
        // TODO: rewrite this query for using join instead of include
        var vacancy = await _context.Vacancies
            .Where(_ => _.Id == id)
            .Include(_ => _.Category)
            .Include(_ => _.Company)
            .Include(_ => _.LocationVacancies).ThenInclude(_ => _.Location)
            .Include(_ => _.VacancySkills).ThenInclude(_ => _.Skill)
            .FirstOrDefaultAsync();
        return vacancy;
    }

    public async Task<IEnumerable<Vacancy>> GetAll()
    {
        // TODO: rewrite this query for using join instead of include
        
        // var vacancies = await (from vacancy in _context.Vacancies
        //     join company in _context.Companies on vacancy.CompanyId equals company.Id
        //     join category in _context.Categories on vacancy.CategoryId equals category.Id
        //     join location in _context.LocationVacancies on vacancy.Id equals location.VacancyId 
        //     join skill in _context.VacancySkills on vacancy.Id equals skill.VacancyId 
        //     select vacancy).ToListAsync(cancellationToken: cancellationToken);
            
            // join location in (from vacancyLocation in _context.LocationVacancies 
            //         join location in _context.Locations on vacancyLocation.LocationId equals location.Id
            //         select new
            //         {
            //             VacancyId = vacancyLocation.VacancyId,
            //             Location = location
            //         }) on vacancy.Id equals location.VacancyId into locations
            // from location in locations.DefaultIfEmpty()
            // join vacancySkill in (from vacancySkill in _context.VacancySkills
            //         join skill in _context.Skills on vacancySkill.SkillId equals skill.Id
            //         select new
            //         {
            //             VacancyId = vacancySkill.VacancyId,
            //             Skill = skill
            //         }) on vacancy.Id equals vacancySkill.VacancyId into skills
            // from skill in skills.DefaultIfEmpty()
            // select new VacancyDTO
            // {
            //     Id = vacancy.Id,
            //     CategoryId = vacancy.CategoryId,
            //     CompanyId = vacancy.CompanyId,
            //     Title = vacancy.Title,
            //     PositionTitle = vacancy.PositionTitle,
            //     Description = vacancy.Description,
            //     Salary = vacancy.Salary,
            //     IsActive = vacancy.IsActive,
            //     CreatedAt = vacancy.CreatedAt,
            //     UpdatedAt = vacancy.UpdatedAt,
            //     Category = category,
            //     Company = company,
            //     Location = location.Location ?? null,
            //     Skills = skill.Skill ?? null
            // };
        // var test = vacancies.ToQueryString();
        var vacancies = await _context.Vacancies
            .Include(_ => _.Company)
            .Include(_ => _.Category)
            .Include(_ => _.LocationVacancies).ThenInclude(_ => _.Location)
            .Include(_ => _.VacancySkills).ThenInclude(_ => _.Skill)
            .ToListAsync();
        return vacancies;
    }

    public async Task<Vacancy> Create(Vacancy vacancy)
    {
        var createdVacancy = await _context.Vacancies.AddAsync(vacancy);
        await _context.SaveChangesAsync();
        return createdVacancy.Entity;
    }

    public async Task<Vacancy> Update(Vacancy vacancy)
    {
        var updatedVacancy = _context.Vacancies.Update(vacancy);
        await _context.SaveChangesAsync();
        return updatedVacancy.Entity;
    }

    public async Task Delete(Vacancy vacancy)
    {
        _context.Vacancies.Remove(vacancy);
        await _context.SaveChangesAsync();
    }
}