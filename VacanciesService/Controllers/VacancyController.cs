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
    public async Task<IActionResult> GetVacancies()
    {
        var result = await _vacanciesService.GetAllVacancies();
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVacancy(Guid id)
    {
        await _vacanciesService.DeleteVacancy(id);
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateVacancy(VacancyCreateDto vacancy)
    {
        var createdVacancy = await _vacanciesService.CreateVacancy(vacancy);
        return Ok(createdVacancy);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateVacancy(VacancyUpdateDto vacancy)
    {
        var updatedVacancy = await _vacanciesService.UpdateVacancy(vacancy);
        return Ok(updatedVacancy);
    }
    
    [HttpPut("{id}/activate-deactivate")]
    public async Task<IActionResult> ActivateDeactivateVacancy(Guid id)
    {
        await _vacanciesService.ActivateDeactivateVacancy(id);
        return Ok();
    }
    
}