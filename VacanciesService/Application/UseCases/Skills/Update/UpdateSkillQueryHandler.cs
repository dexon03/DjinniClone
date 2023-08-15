using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.Update;

public class UpdateSkillQueryHandler : IRequestHandler<UpdateSkillQuery,Skill>
{
    private readonly IRepository _repository;

    public UpdateSkillQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Skill> Handle(UpdateSkillQuery request, CancellationToken cancellationToken)
    {
        var result = _repository.Update(request.Skill);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }
}