using FluentValidation;

namespace VacanciesService.Application.UseCases.Locations.Delete;

public class DeleteLocationValidator : AbstractValidator<DeleteLocationCommand>
{
    public DeleteLocationValidator()
    {
        RuleFor(x => x.Location.Id).NotEmpty();
        RuleFor(x => x.Location.City).NotEmpty();
        RuleFor(x => x.Location.Country).NotEmpty();
    }
}