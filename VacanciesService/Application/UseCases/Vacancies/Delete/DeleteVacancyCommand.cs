using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.Delete;

public record DeleteVacancyCommand(Vacancy vacancy) : IRequest;