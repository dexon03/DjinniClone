using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.Get;

public record GetCategoryQuery(Guid id) : IRequest<Category>;