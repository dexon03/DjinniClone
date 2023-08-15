using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Update;

public record UpdateVacancyQuery(Vacancy vacancy) : IRequest<Vacancy>;