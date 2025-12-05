namespace TaskManager.Communication.Responses;
public sealed class ErrorResponse
{
    public int Status { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Message { get; init; }
    public IReadOnlyList<string>? Errors { get; init; }
}