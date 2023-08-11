using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Delete;

public record DeleteVacancyCommand(Guid Id) : IRequest;