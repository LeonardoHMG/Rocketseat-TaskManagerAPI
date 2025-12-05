namespace TaskManager.Application.Validation;
public sealed class ValidationError
{
    public string Message { get; init; }

    public ValidationError(string message)
    {
        Message = message ?? string.Empty;
    }
}
