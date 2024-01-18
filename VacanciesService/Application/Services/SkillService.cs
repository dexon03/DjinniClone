using AutoMapper;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.Services;

public class SkillService : ISkillService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public SkillService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public Task<List<Skill>> GetAllSkills(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<Skill>().ToListAsync(cancellationToken);
    }

    public async Task<Skill> GetSkillById(Guid id)
    {
        var skill = await _repository.GetByIdAsync<Skill>(id);
        if (skill == null)
        {
            throw new Exception($"Skill not found");
        }
        
        return skill;
    }

    public async Task<Skill> CreateSkill(SkillCreateDto skill, CancellationToken cancellationToken = default)
    {
        var skillEntity = _mapper.Map<Skill>(skill);
        var result = await _repository.CreateAsync(skillEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Skill> UpdateSkill(SkillUpdateDto skill, CancellationToken cancellationToken = default)
    {
        var skillEntity = _mapper.Map<Skill>(skill);
        var isExists = await _repository.AnyAsync<Skill>(x => x.Id == skillEntity.Id);
        if (!isExists)
        {
            throw new Exception("Skill not found");
        }
        
        var result = _repository.Update(skillEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task DeleteSkill(Guid id, CancellationToken cancellationToken = default)
    {
        var skill = await _repository.GetByIdAsync<Skill>(id);
        if (skill == null)
        {
            throw new Exception($"Skill not found");
        }
        
        _repository.Delete(skill);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteManySkills(Skill[] skills, CancellationToken cancellationToken = default)
    {
        _repository.DeleteRange(skills);
        return _repository.SaveChangesAsync(cancellationToken);
    }
}