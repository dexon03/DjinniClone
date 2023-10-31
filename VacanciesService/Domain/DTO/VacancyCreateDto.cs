using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.DTO;

public class VacancyCreateDto
{
    public Guid CategoryId { get; set; }
    public Guid CompanyId { get; set; }
    public string Title { get; set; }
    public string PositionTitle { get; set; }
    public string Description { get; set; }
    public double Salary { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Guid> LocationIds { get; set; }
    public List<Guid> SkillIds { get; set; }
}

public static class VacancyMap
{
    public static Vacancy MapCreate(this Vacancy vacancy, VacancyCreateDto vacancyCreateDto)
    {
        if (vacancy.Id == Guid.Empty)
        {
            vacancy.Id = Guid.NewGuid();
        }
        vacancy.CategoryId = vacancyCreateDto.CategoryId;
        vacancy.CompanyId = vacancyCreateDto.CompanyId;
        vacancy.Title = vacancyCreateDto.Title;
        vacancy.PositionTitle = vacancyCreateDto.PositionTitle;
        vacancy.Description = vacancyCreateDto.Description;
        vacancy.Salary = vacancyCreateDto.Salary;
        vacancy.IsActive = vacancyCreateDto.IsActive;
        vacancy.VacancySkill = vacancyCreateDto.SkillIds.Select(skillId => new VacancySkill
        {
            VacancyId = vacancy.Id,
            SkillId = skillId
        }).ToList();
        vacancy.LocationVacancy = vacancyCreateDto.LocationIds.Select(locationId => new LocationVacancy
        {
            VacancyId = vacancy.Id,
            LocationId = locationId
        }).ToList();
        return vacancy;
    }
}