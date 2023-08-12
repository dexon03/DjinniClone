using Core.Database;
using MediatR;
using VacanciesService.Application.UseCases.Locations.Delete;

namespace VacanciesService.Application.UseCases.Locations.DeleteMany;

public class DeleteManyLocationCommandHandler : IRequestHandler<DeleteManyLocationCommand>
{
    private readonly IRepository _repository;

    public DeleteManyLocationCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task Handle(DeleteManyLocationCommand request, CancellationToken cancellationToken)
    {
        _repository.DeleteRange(request.Locations);
        return _repository.SaveChangesAsync(cancellationToken);
    }
}