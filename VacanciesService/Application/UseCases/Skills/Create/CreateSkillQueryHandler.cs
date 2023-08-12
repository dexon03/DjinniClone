using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.Create;

public class CreateSkillQueryHandler : IRequestHandler<CreateSkillQuery,Skill>
{
    private readonly IRepository _repository;

    public CreateSkillQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Skill> Handle(CreateSkillQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.CreateAsync(request.Skill);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }
}