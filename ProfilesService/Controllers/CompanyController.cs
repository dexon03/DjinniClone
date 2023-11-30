using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;

namespace ProfilesService.Controllers;

public class CompanyController : BaseController
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
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
    
    [Authorize(Roles = "Admin, CompanyOwner")]
    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateDto company)
    {
        return Ok(await _companyService.CreateCompany(company));
    }
    
    [Authorize(Roles = "Admin, CompanyOwner")]
    [HttpPut]
    public async Task<IActionResult> Update(CompanyUpdateDto company)
    {
        return Ok(await _companyService.UpdateCompany(company));
    }
    
    [Authorize(Roles = "Admin, CompanyOwner")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _companyService.DeleteCompany(id);
        return Ok();
    }
}