using VacanciesService.Domain.DTO;

namespace VacanciesService.Domain.Contacts;

public interface IStatisticService
{
    Task<IEnumerable<StatisticNode>> GetStatisticAsync(string? filterSkill);
    Task<List<StatisticNode>> GetMockedStatisticAsync();
}