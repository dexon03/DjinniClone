using Core.Database;
using MediatR;

namespace VacanciesService.Application.UseCases.Skills.DeleteMany;

public class DeleteManyCommandHandler : IRequestHandler<DeleteManyCommand>
{
    private readonly IRepository _repository;

    public DeleteManyCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task Handle(DeleteManyCommand request, CancellationToken cancellationToken)
    {
        _repository.DeleteRange(request.Skills);
        return _repository.SaveChangesAsync(cancellationToken);
    }
}