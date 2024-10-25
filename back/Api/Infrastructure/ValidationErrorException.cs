using FluentValidation.Results;

namespace Api.Infrastructure;

public class ValidationErrorException : Exception
{
    private readonly string _message;

    public ValidationErrorException(IList<ValidationFailure> failures)
    {
        var Errors = failures
            .Select(error => $"'{error.PropertyName}': {error.ErrorMessage};")
            .ToArray();

        _message = string.Join(Environment.NewLine, Errors);
    }

    public override string Message => _message;
}
