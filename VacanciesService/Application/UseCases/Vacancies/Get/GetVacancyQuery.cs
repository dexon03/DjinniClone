using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Get;

public record GetVacancyQuery(Guid Id) : IRequest<Vacancy>;