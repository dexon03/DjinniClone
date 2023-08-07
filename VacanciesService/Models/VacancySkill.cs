namespace VacanciesService.Models;

public class VacancySkill
{
    public Guid Id { get; set; }
    public Guid VacancyId { get; set; }
    public Guid SkillId { get; set; }
    public virtual Skill Skill { get; set; } = null!;
    public virtual Vacancy Vacancy { get; set; } = null!;
}