using TaskManager.Communication.Requests;

namespace TaskManager.Application.Validation;
public static class TaskValidator
{
    private static readonly string[] AllowedPriorities = new[] { "high", "medium", "low" };
    private static readonly string[] AllowedStatuses = new[] { "pending", "inprogress", "completed" };

    public static void Validate(RequestTaskJson request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var errors = new List<ValidationError>();

        
        if (string.IsNullOrWhiteSpace(request.Name))
            errors.Add(new ValidationError("Name is required."));
        else if (request.Name.Trim().Length > 100)
            errors.Add(new ValidationError("Name must be at most 100 characters."));

       
        if (!string.IsNullOrEmpty(request.Description) && request.Description.Trim().Length > 500)
            errors.Add(new ValidationError("Description must be at most 500 characters."));

        
        if (string.IsNullOrWhiteSpace(request.Priority))
        {
            errors.Add(new ValidationError("Priority is required."));
        }
        else
        {
            var p = request.Priority.Trim().ToLowerInvariant();
            if (!AllowedPriorities.Contains(p))
                errors.Add(new ValidationError($"Priority must be one of: {string.Join(", ", AllowedPriorities)}."));
        }
        
        if (string.IsNullOrWhiteSpace(request.Status))
        {
            errors.Add(new ValidationError("Status is required."));
        }
        else
        {
            var s = request.Status.Trim().ToLowerInvariant();
            if (!AllowedStatuses.Contains(s))
                errors.Add(new ValidationError($"Status must be one of: pending, inProgress, completed."));
        }

        var now = DateTime.UtcNow;
        if (request.DueDate == default)
        {
            errors.Add(new ValidationError("DueDate is required and must be a valid date."));
        }
        else if (request.DueDate <= now)
        {
            errors.Add(new ValidationError("DueDate must be a future date/time."));
        }

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }

}
