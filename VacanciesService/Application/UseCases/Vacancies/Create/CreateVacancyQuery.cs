using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Create;

public record CreateVacancyQuery(Vacancy vacancy) : IRequest<Vacancy>; 