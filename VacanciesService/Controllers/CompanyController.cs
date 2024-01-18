using Microsoft.AspNetCore.Mvc;
using VacanciesService.Domain.Contacts;

namespace VacanciesService.Controllers;

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
}