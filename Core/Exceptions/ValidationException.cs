using FluentValidation.Results;

namespace Core.Exceptions;

public class ValidationException : Exception
{
    private readonly List<ValidationFailure> _errors;

    public ValidationException(List<ValidationFailure> errors) : base(GetErrorMessage(errors))
    {
        _errors = errors;
    }
    
    private static string GetErrorMessage(List<ValidationFailure> _errors)
    {
        return string.Join(";  |  ", _errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));
    }
}

public record ValidationError(string PropertyName, string ErrorMessage);