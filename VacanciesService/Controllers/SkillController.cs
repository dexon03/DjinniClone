using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Application.UseCases.Skills.Create;
using VacanciesService.Application.UseCases.Skills.Delete;
using VacanciesService.Application.UseCases.Skills.DeleteMany;
using VacanciesService.Application.UseCases.Skills.Get;
using VacanciesService.Application.UseCases.Skills.GetAll;
using VacanciesService.Application.UseCases.Skills.Update;
using VacanciesService.Domain.Models;

namespace VacanciesService.Controllers;

public class SkillController : BaseController
{
    public SkillController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllSkillsQuery());
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSkillQuery(id));
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Skill skill)
    {
        await _mediator.Send(new DeleteSkillCommand(skill));
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Skill skill)
    {
        var result = await _mediator.Send(new CreateSkillQuery(skill));
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Skill skill)
    {
        var result = await _mediator.Send(new UpdateSkillQuery(skill));
        return Ok(result);
    }
    
    [HttpDelete("many")]
    public async Task<IActionResult> DeleteMany(Skill[] skills)
    {
        await _mediator.Send(new DeleteManyCommand(skills));
        return Ok();
    }
}