namespace VacanciesService.Domain.Models;

public class Skill
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<VacancySkill> VacancySkill { get; set; }
}