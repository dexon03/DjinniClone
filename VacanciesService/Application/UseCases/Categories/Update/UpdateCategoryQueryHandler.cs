using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.Update;

public class UpdateCategoryQueryHandler : IRequestHandler<UpdateCategoryQuery,Category>
{
    private readonly IRepository _repository;

    public UpdateCategoryQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async  Task<Category> Handle(UpdateCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = _repository.Update(request.category);
        await _repository.SaveChangesAsync(cancellationToken);
        return category;
    }
}