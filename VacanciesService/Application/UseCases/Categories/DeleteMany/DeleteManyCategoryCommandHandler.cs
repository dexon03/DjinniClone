using Core.Database;
using MediatR;
using VacanciesService.Application.UseCases.Categories.Delete;

namespace VacanciesService.Application.UseCases.Categories.DeleteMany;

public class DeleteManyCategoryCommandHandler : IRequestHandler<DeleteManyCategoryCommand>
{
    private readonly IRepository _repository;

    public DeleteManyCategoryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task Handle(DeleteManyCategoryCommand request, CancellationToken cancellationToken)
    {
        _repository.DeleteRange(request.categories);
        return _repository.SaveChangesAsync(cancellationToken);
    }
}