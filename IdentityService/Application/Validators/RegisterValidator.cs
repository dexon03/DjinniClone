using Core.Database;
using FluentValidation;
using IdentityService.Domain.Dto;
using IdentityService.Domain.Models;

namespace IdentityService.Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator(IRepository repository)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(50)
            .WithMessage("First name must not exceed 50 characters");
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom((email, context) =>
            {
                if (repository.FirstOrDefault<User>( x => x.Email == email) != null)
                {
                    context.AddFailure("Email should be unique");
                }
            });;
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one non alphanumeric character.");
    }
}