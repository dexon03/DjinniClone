using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.Update;

public record UpdateSkillQuery(Skill Skill) : IRequest<Skill>;