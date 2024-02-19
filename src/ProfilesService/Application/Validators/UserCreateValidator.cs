using FluentValidation;
using ProfilesService.Domain.DTO;

namespace ProfilesService.Application.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
        RuleFor(x => x.UserRole).NotEmpty().WithMessage("UserRole is required");
    }
}
