using MediatR;

namespace VacanciesService.Application.UseCases.Vacancies.ActivateDeactivate;

public record ActivateDeactivateVacancyCommand(Guid Id) : IRequest;