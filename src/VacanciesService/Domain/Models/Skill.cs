using System.Text.Json.Serialization;

namespace VacanciesService.Domain.Models;

public class Skill
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]public ICollection<VacancySkill> VacancySkill { get; set; }
}