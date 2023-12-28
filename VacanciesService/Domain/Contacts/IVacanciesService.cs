using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.Contacts;

public interface IVacanciesService
{
    Task<List<VacancyGetAllDto>> GetAllVacancies(VacancyFilterParameters vacancyFilter, CancellationToken cancellationToken = default);
    Task<VacancyGetDto> GetVacancyById(Guid id, CancellationToken cancellationToken = default);
    Task<List<VacancyGetAllDto>> GetVacanciesByRecruiterId(Guid recruiterId,VacancyFilterParameters vacancyFilter,CancellationToken cancellationToken = default);
    Task<Vacancy> CreateVacancy(VacancyCreateDto vacancyDto, CancellationToken cancellationToken = default);
    Task<Vacancy> UpdateVacancy(VacancyUpdateDto vacancy, CancellationToken cancellationToken = default);
    Task DeleteVacancy(Guid id, CancellationToken cancellationToken = default);
    Task ActivateDeactivateVacancy(Guid id, CancellationToken cancellationToken = default);
}