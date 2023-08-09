using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Vacancies.GetAll;

public record GetAllVacanciesQuery() : IRequest<List<Vacancy>>;