using TaskManager.Application.Validation;

namespace TaskManager.Application.Exceptions;
public sealed class ValidationException : AppException
{
    public IReadOnlyList<ValidationError> Errors { get; }

    public ValidationException(IEnumerable<ValidationError> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = new List<ValidationError>(errors ?? Array.Empty<ValidationError>());
    }
}