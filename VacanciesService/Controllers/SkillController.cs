using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Controllers;

public class SkillController : BaseController
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _skillService.GetAllSkills();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _skillService.GetSkillById(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SkillCreateDto skill)
    {
        var result = await _skillService.CreateSkill(skill);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(SkillUpdateDto skill)
    {
        var result = await _skillService.UpdateSkill(skill);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _skillService.DeleteSkill(id);
        return Ok();
    }
    
    [HttpDelete("many")]
    public async Task<IActionResult> DeleteMany(Skill[] skills)
    {
        await _skillService.DeleteManySkills(skills);
        return Ok();
    }
}