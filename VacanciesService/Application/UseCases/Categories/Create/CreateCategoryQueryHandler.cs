using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.Create;

public class CreateCategoryQueryHandler : IRequestHandler<CreateCategoryQuery,Category>
{
    private readonly IRepository _repository;

    public CreateCategoryQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Category> Handle(CreateCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.CreateAsync(request.category);
        await _repository.SaveChangesAsync(cancellationToken);
        return category;
    }
}