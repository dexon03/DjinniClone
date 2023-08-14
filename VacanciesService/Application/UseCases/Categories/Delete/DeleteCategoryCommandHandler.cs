using Core.Database;
using MediatR;

namespace VacanciesService.Application.UseCases.Categories.Delete;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IRepository _repository;

    public DeleteCategoryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        _repository.Delete(request.Category);
        return _repository.SaveChangesAsync(cancellationToken);
    }
}