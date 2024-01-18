using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Domain.Contracts;

public interface ICompanyService
{
    Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken = default);
    Task<Company> GetCompanyById(Guid id);
    Task<Company> CreateCompany(CompanyCreateDto company, CancellationToken cancellationToken = default);
    Task<Company> UpdateCompany(CompanyUpdateDto company, CancellationToken cancellationToken = default);
    Task DeleteCompany(Guid id, CancellationToken cancellationToken = default);
}