using FluentValidation;

namespace VacanciesService.Application.UseCases.Categories.Create;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryQuery>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.category.Name).NotEmpty().MinimumLength(2);
    }
}