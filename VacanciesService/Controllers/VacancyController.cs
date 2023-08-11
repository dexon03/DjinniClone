using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Application.UseCases.Vacancies.ActivateDeactivate;
using VacanciesService.Application.UseCases.Vacancies.Create;
using VacanciesService.Application.UseCases.Vacancies.Delete;
using VacanciesService.Application.UseCases.Vacancies.Get;
using VacanciesService.Application.UseCases.Vacancies.GetAll;
using VacanciesService.Application.UseCases.Vacancies.Update;
using VacanciesService.Domain.Models;

namespace VacanciesService.Controllers;

public class VacancyController : BaseController
{
    public VacancyController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVacancyById(Guid id)
    {
        var result = await _mediator.Send(new GetVacancyQuery(id));
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetVacancies()
    {
        var result = await _mediator.Send(new GetAllVacanciesQuery());
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVacancy(Guid id)
    {
        await _mediator.Send(new DeleteVacancyCommand(id));
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateVacancy(Vacancy vacancy)
    {
        var createdVacancy = await _mediator.Send(new CreateVacancyQuery(vacancy));
        return Ok(createdVacancy);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateVacancy(Vacancy vacancy)
    {
        var updatedVacancy = await _mediator.Send(new UpdateVacancyQuery(vacancy));
        return Ok(updatedVacancy);
    }
    
    [HttpPost("{id}/activate-deactivate")]
    public async Task<IActionResult> ActivateDeactivateVacancy(Guid id)
    {
        await _mediator.Send(new ActivateDeactivateVacancyCommand(id));
        return Ok();
    }
    
}