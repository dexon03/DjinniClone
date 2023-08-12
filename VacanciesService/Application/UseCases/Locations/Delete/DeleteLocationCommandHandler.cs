using Core.Database;
using MediatR;

namespace VacanciesService.Application.UseCases.Locations.Delete;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand>
{
    private readonly IRepository _repository;

    public DeleteLocationCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        _repository.Delete(request.Location);
        return _repository.SaveChangesAsync(cancellationToken);
    }
}