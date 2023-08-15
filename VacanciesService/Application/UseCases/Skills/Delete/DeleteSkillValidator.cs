using FluentValidation;

namespace VacanciesService.Application.UseCases.Skills.Delete;

public class DeleteSkillValidator : AbstractValidator<DeleteSkillCommand>
{
    public DeleteSkillValidator()
    {
        RuleFor(x => x.Skill.Id).NotEmpty();
        RuleFor(x => x.Skill.Name).NotEmpty();
    }
}