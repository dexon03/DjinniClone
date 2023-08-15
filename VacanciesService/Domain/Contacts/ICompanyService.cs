using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.Contacts;

public interface ICompanyService
{
    Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken = default);
    Task<Company> GetCompanyById(Guid id, CancellationToken cancellationToken = default);
    Task<Company> CreateCompany(CompanyCreateDto company, CancellationToken cancellationToken = default);
    Task<Company> UpdateCompany(CompanyUpdateDto company, CancellationToken cancellationToken = default);
    Task DeleteCompany(Guid id, CancellationToken cancellationToken = default);
    
    
}