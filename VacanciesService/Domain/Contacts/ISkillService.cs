using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.Contacts;

public interface ISkillService
{
    Task<List<Skill>> GetAllSkills(CancellationToken cancellationToken = default);
    Task<Skill> GetSkillById(Guid id, CancellationToken cancellationToken = default);
    Task<Skill> CreateSkill(SkillCreateDto skill, CancellationToken cancellationToken = default);
    Task<Skill> UpdateSkill(SkillUpdateDto skill, CancellationToken cancellationToken = default);
    Task DeleteSkill(Guid id, CancellationToken cancellationToken = default);
    Task DeleteManySkills(Skill[] skills, CancellationToken cancellationToken = default);
}