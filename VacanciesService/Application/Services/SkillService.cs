using AutoMapper;
using Core.Database;
using Core.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace VacanciesService.Application.Services;

public class SkillService : ISkillService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<SkillCreateDto> _createValidator;
    private readonly IValidator<SkillUpdateDto> _updateValidator;

    public SkillService(IRepository repository, 
        IMapper mapper,
        IValidator<SkillCreateDto> createValidator, 
        IValidator<SkillUpdateDto> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }
    public Task<List<Skill>> GetAllSkills(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<Skill>().ToListAsync(cancellationToken);
    }

    public async Task<Skill> GetSkillById(Guid id, CancellationToken cancellationToken = default)
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
        var validationResult = await _createValidator.ValidateAsync(skill, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var skillEntity = _mapper.Map<Skill>(skill);
        var result = await _repository.CreateAsync(skillEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Skill> UpdateSkill(SkillUpdateDto skill, CancellationToken cancellationToken = default)
    {
        var validationResult = await _updateValidator.ValidateAsync(skill, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
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