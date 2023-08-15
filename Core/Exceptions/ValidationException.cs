namespace Core.Exceptions;

public class ValidationException : Exception
{
    private readonly IReadOnlyCollection<ValidationError> _errors;

    public ValidationException(IReadOnlyCollection<ValidationError> errors) : base("Validation failed: " + GetErrorMessage(errors))
    {
        _errors = errors;
    }
    
    private static string GetErrorMessage(IReadOnlyCollection<ValidationError> _errors)
    {
        return string.Join("; | ", _errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}"));
    }
}

public record ValidationError(string PropertyName, string ErrorMessage);