using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;

namespace VacanciesService.Controllers;

public class VacancyController : BaseController
{
    private readonly IVacanciesService _vacanciesService;
    private readonly IOllamaApiClient _ollamaApiClient;

    public VacancyController(IVacanciesService vacanciesService, IOllamaApiClient ollamaApiClient)
    {
        _vacanciesService = vacanciesService;
        _ollamaApiClient = ollamaApiClient;
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
    [HttpPost("getDescription")]
    public async Task<IActionResult> GetGeneratedVacancyDescription(GenerateVacancyDescription request)
    { 
        string result = string.Empty;
        var prompt = "Generate description for vacancy in english. Max word count is 1000. " +
                     "!!!Important " +
                     "Return only vacancy description" +
                     "!!! The vacancy is for a " +
                     request.Title + " at " + request.CompanyDescription + ". The job description is " +
                     request.VacancyShortDescription;
        await foreach (var stream in _ollamaApiClient.GenerateAsync(prompt))
            result += stream.Response;
        
        return Ok(new
        {
            Description = result
        });
    }
}