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
        return Ok(await _vacanciesService.GetVacancyById(id));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetVacancies([FromQuery]VacancyFilterParameters vacancyFilter, CancellationToken cancellationToken)
    {
        return Ok(await _vacanciesService.GetAllVacancies(vacancyFilter,cancellationToken));
    }
    
    [HttpGet("getRecruiterVacancies/{recruiterId}")]
    public async Task<IActionResult> GetRecruiterVacancies(Guid recruiterId, [FromQuery]VacancyFilterParameters vacancyFilter,CancellationToken cancellationToken)
    {
        return Ok(await _vacanciesService.GetVacanciesByRecruiterId(recruiterId,vacancyFilter, cancellationToken));
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
        return Ok(await _vacanciesService.CreateVacancy(vacancy));
    }
    
    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpPut]
    public async Task<IActionResult> UpdateVacancy(VacancyUpdateDto vacancy)
    {
        return Ok(await _vacanciesService.UpdateVacancy(vacancy));
    }
    
    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpPut("{id}/activate-deactivate")]
    public async Task<IActionResult> ActivateDeactivateVacancy(Guid id)
    {
        await _vacanciesService.ActivateDeactivateVacancy(id);
        return Ok();
    }
    
    [Authorize(Roles = "Recruiter")]
    [HttpGet("getDescription")]
    public IActionResult GetGeneratedVacancyDescriprion()
    { 
        return Ok("*generated*");
    }
}