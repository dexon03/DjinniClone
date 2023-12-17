using Core.Database;
using FluentValidation;
using IdentityService.Domain.Dto;
using IdentityService.Domain.Models;

namespace IdentityService.Application.Validators;

public class UserUpdateValidator : AbstractValidator<UpdateUserRequest>
{
    public UserUpdateValidator(IRepository repository)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(50)
            .WithMessage("First name must not exceed 50 characters");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MaximumLength(50)
            .WithMessage("Last name must not exceed 50 characters");
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom((email, context) =>
            {
                if (repository.Any<User>( x => x.Email == email))
                {
                    context.AddFailure("Email should be unique");
                }
            });
    }
}