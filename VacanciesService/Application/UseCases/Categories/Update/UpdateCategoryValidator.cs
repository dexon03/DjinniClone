using FluentValidation;

namespace VacanciesService.Application.UseCases.Categories.Update;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryQuery>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.category.Id).NotEmpty();
        RuleFor(x => x.category.Name).NotEmpty().MinimumLength(2);
    }
}