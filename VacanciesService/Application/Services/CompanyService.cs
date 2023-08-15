using System.Net;
using AutoMapper;
using Core.Database;
using Core.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CompanyCreateDto> _createValidator;
    private readonly IValidator<CompanyUpdateDto> _updateValidator;

    public CompanyService(IRepository repository, IMapper mapper, IValidator<CompanyCreateDto> createValidator, IValidator<CompanyUpdateDto> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
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
        var validationResult = await _createValidator.ValidateAsync(company,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ExceptionWithStatusCode(string.Join(";  |  ",validationResult.Errors.Select(e => e.ErrorMessage)), HttpStatusCode.BadRequest);
        }
        
        var companyEntity = _mapper.Map<Company>(company);
        var result = await _repository.CreateAsync(companyEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Company> UpdateCompany(CompanyUpdateDto company, CancellationToken cancellationToken = default)
    {
        var validationResult = await _updateValidator.ValidateAsync(company,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ExceptionWithStatusCode(string.Join(";  |  ",validationResult.Errors.Select(e => e.ErrorMessage)), HttpStatusCode.BadRequest);
        }
        
        var companyEntity = _mapper.Map<Company>(company);
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