using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.Contacts;

public interface IVacanciesService
{
    Task<List<Vacancy>> GetAllVacancies(CancellationToken cancellationToken = default);
    Task<Vacancy> GetVacancyById(Guid id, CancellationToken cancellationToken = default);
    Task<Vacancy> CreateVacancy(VacancyCreateDto vacancy, CancellationToken cancellationToken = default);
    Task<Vacancy> UpdateVacancy(VacancyUpdateDto vacancy, CancellationToken cancellationToken = default);
    Task DeleteVacancy(Guid id, CancellationToken cancellationToken = default);
    Task ActivateDeactivateVacancy(Guid id, CancellationToken cancellationToken = default);
}