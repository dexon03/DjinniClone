using Core.Database;
using FluentValidation;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Application.Validators;

public class ProfileCreateValidator : AbstractValidator<ProfileCreateDto>
{
    public ProfileCreateValidator(IRepository repository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
        RuleFor(x => x.PositionTitle).NotEmpty().WithMessage("PositionTitle is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("ImageUrl is required");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
        RuleFor(x => x.DateBirth).NotEmpty().WithMessage("DateBirth is required");
        RuleFor(x => x.WorkExperience).NotEmpty().WithMessage("WorkExperience is required");
        RuleFor(x => x.DesiredSalary).NotEmpty().WithMessage("DesiredSalary is required");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Linked user is required").Custom((id, context) =>
        {
            if (repository.GetById<User>(id) == null)
            {
                context.AddFailure("User not found");
            }
        });
    }
}
