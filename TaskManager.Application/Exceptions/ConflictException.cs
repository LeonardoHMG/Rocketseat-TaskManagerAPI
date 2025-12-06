namespace TaskManager.Application.Exceptions;
public class ConflictException : AppException
{
    public ConflictException() { }

    public ConflictException(string message) : base(message) { }
}
