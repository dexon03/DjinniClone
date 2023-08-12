using Core.Database;
using MediatR;

namespace VacanciesService.Application.UseCases.Skills.Delete;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand>
{
    private readonly IRepository _repository;

    public DeleteSkillCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        _repository.Delete(request.Skill);
        return _repository.SaveChangesAsync(cancellationToken);
    }
}