using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.GetAll;

public record GetAllSkillsQuery() : IRequest<List<Skill>>;