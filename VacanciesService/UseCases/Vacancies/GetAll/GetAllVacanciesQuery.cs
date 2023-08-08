using MediatR;
using VacanciesService.Models;
using VacanciesService.Models.DTO;

namespace VacanciesService.UseCases.Vacancies.GetAll;

public record GetAllVacanciesQuery() : IRequest<List<Vacancy>>;