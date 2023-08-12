using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.Get;

public class GetSkillQueryHandler : IRequestHandler<GetSkillQuery, Skill>
{
    private readonly IRepository _repository;

    public GetSkillQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Skill> Handle(GetSkillQuery request, CancellationToken cancellationToken)
    {
        var skill = await _repository.GetByIdAsync<Skill>(request.id);
        if (skill == null)
        {
            throw new Exception("Skill not found");
        }
        
        return skill;
    }
}