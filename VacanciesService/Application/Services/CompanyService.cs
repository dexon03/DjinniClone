using AutoMapper;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public CompanyService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<List<Company>> GetAllCompanies(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<Company>().ToListAsync(cancellationToken);
    }

    public async Task<Company> GetCompanyById(Guid id, CancellationToken cancellationToken = default)
    {
        var company = await _repository.GetByIdAsync<Company>(id);
        if (company == null)
        {
            throw new Exception("Company not found");
        }
        
        return company;
    }

    public async Task<Company> CreateCompany(CompanyCreateDto company, CancellationToken cancellationToken = default)
    {
        var companyEntity = _mapper.Map<Company>(company);
        var result = await _repository.CreateAsync(companyEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Company> UpdateCompany(CompanyUpdateDto company, CancellationToken cancellationToken = default)
    {
        var companyEntity = _mapper.Map<Company>(company);
        var isExist = await _repository.AnyAsync<Company>(x => x.Id == companyEntity.Id);
        if (!isExist)
        {
            throw new Exception("Company that you trying to update, not exist");
        }
        
        var result = _repository.Update(companyEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task DeleteCompany(Guid id, CancellationToken cancellationToken = default)
    {
        var company = await _repository.GetByIdAsync<Company>(id);
        if (company == null)
        {
            throw new Exception("Company not found");
        }
        
        _repository.Delete(company);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}