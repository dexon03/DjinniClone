using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.GetAll;

public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery,List<Skill>>
{
    private readonly IRepository _repository;

    public GetAllSkillsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task<List<Skill>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = _repository.GetAll<Skill>();
        
        return Task.FromResult(skills.ToList());
    }
}