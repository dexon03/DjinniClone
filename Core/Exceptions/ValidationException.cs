namespace Core.Exceptions;

public class ValidationException : Exception
{
    private readonly IReadOnlyCollection<ValidationError> _errors;

    public ValidationException(IReadOnlyCollection<ValidationError> errors) : base ("Validation failed")
    {
        _errors = errors;
    }
}

public record ValidationError(string PropertyName, string ErrorMessage);