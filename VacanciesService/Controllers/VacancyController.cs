using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
namespace VacanciesService.Controllers;

public class VacancyController : BaseController
{
    private readonly IVacanciesService _vacanciesService;

    public VacancyController(IVacanciesService vacanciesService)
    {
        _vacanciesService = vacanciesService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVacancyById(Guid id)
    {
        var result = await _vacanciesService.GetVacancyById(id);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetVacancies(CancellationToken cancellationToken)
    {
        var result = await _vacanciesService.GetAllVacancies(cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("getRecruiterVacancies/{recruiterId}")]
    public async Task<IActionResult> GetRecruiterVacancies(Guid recruiterId, CancellationToken cancellationToken)
    {
        var result = await _vacanciesService.GetVacanciesByRecruiterId(recruiterId,cancellationToken);
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVacancy(Guid id)
    {
        await _vacanciesService.DeleteVacancy(id);
        return Ok();
    }
    
    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpPost]
    public async Task<IActionResult> CreateVacancy(VacancyCreateDto vacancy)
    {
        var createdVacancy = await _vacanciesService.CreateVacancy(vacancy);
        return Ok(createdVacancy);
    }
    
    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpPut]
    public async Task<IActionResult> UpdateVacancy(VacancyUpdateDto vacancy)
    {
        var updatedVacancy = await _vacanciesService.UpdateVacancy(vacancy);
        return Ok(updatedVacancy);
    }
    
    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpPut("{id}/activate-deactivate")]
    public async Task<IActionResult> ActivateDeactivateVacancy(Guid id)
    {
        await _vacanciesService.ActivateDeactivateVacancy(id);
        return Ok();
    }
    
}