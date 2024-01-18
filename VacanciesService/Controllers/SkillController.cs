using Microsoft.AspNetCore.Authorization;
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
        return Ok(await _skillService.GetAllSkills());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _skillService.GetSkillById(id));
    }

    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpPost]
    public async Task<IActionResult> Create(SkillCreateDto skill)
    {
        return Ok(await _skillService.CreateSkill(skill));
    }

    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpPut]
    public async Task<IActionResult> Update(SkillUpdateDto skill)
    {
        return Ok(await _skillService.UpdateSkill(skill));
    }
    
    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _skillService.DeleteSkill(id);
        return Ok();
    }
    
    [Authorize(Roles = "Admin, Recruiter, CompanyOwner")]
    [HttpDelete("many")]
    public async Task<IActionResult> DeleteMany(Skill[] skills)
    {
        await _skillService.DeleteManySkills(skills);
        return Ok();
    }
}