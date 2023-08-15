using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;

namespace VacanciesService.Controllers;

public class CompanyController : BaseController
{
    private readonly ICompanyService _companyService;

    public CompanyController(IMediator mediator, ICompanyService companyService) : base(mediator)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _companyService.GetAllCompanies());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _companyService.GetCompanyById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateDto company)
    {
        return Ok(await _companyService.CreateCompany(company));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(CompanyUpdateDto company)
    {
        return Ok(await _companyService.UpdateCompany(company));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _companyService.DeleteCompany(id);
        return Ok();
    }
    
}