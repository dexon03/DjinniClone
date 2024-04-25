using FluentValidation.Results;

namespace Core.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(List<ValidationFailure> errors) : base(GetErrorMessage(errors))
    {
    }
    
    private static string GetErrorMessage(List<ValidationFailure> errors)
    {
        return string.Join(";  |  ", errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));
    }
}