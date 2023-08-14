using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.Delete;

public record DeleteCategoryCommand(Category Category) : IRequest;