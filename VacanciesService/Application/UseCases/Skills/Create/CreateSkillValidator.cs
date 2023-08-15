using FluentValidation;

namespace VacanciesService.Application.UseCases.Skills.Create;

public class CreateSkillValidator : AbstractValidator<CreateSkillQuery>
{
    public CreateSkillValidator()
    {
        RuleFor(x => x.Skill);
        RuleFor(x => x.Skill.Name).MinimumLength(3);
    }
}