namespace VacanciesService.Domain.Models;

public class VacancySkill
{
    public Guid VacancyId { get; set; }
    public Guid SkillId { get; set; }
    public Skill Skill { get; set; } = null!;
    public Vacancy Vacancy { get; set; } = null!;
}