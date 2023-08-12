using FluentValidation;

namespace VacanciesService.Application.UseCases.Skills.Update;

public class UpdateSkillValidator : AbstractValidator<UpdateSkillQuery>
{
    public UpdateSkillValidator()
    {
        RuleFor(x => x.Skill);
        RuleFor(x => x.Skill.Name).MinimumLength(3);
    }
}