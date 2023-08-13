using FluentValidation;

namespace VacanciesService.Application.UseCases.Locations.Update;

public class UpdateLocationValidator : AbstractValidator<UpdateLocationQuery>
{
    public UpdateLocationValidator()
    {
        RuleFor(x => x.location.Id).NotEmpty();
        RuleFor(x => x.location.City).MinimumLength(2);
        RuleFor(x => x.location.Country).MinimumLength(2);
    }
}