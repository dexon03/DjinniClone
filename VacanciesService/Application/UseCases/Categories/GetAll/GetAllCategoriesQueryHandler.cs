using Core.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.GetAll;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<Category>>
{
    private readonly IRepository _repository;

    public GetAllCategoriesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task<List<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAll<Category>().ToListAsync(cancellationToken);
    }
}