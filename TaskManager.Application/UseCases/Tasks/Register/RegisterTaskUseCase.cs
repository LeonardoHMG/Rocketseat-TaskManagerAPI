using TaskManager.Application.AppServices;
using TaskManager.Application.Validation;
using TaskManager.Communication.Requests;
using TaskManager.Communication.Responses;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.UseCases.Tasks.Register;
public class RegisterTaskUseCase
{
    public ResponseRegisteredTaskJson Execute(RequestTaskJson request)
    {
        TaskValidator.Validate(request);

        var newTask = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Description = request.Description?.Trim() ?? string.Empty,
            Priority = request.Priority.Trim(),
            DueDate = request.DueDate,
            Status = request.Status.Trim()
        };

        TaskAppService.Create(newTask);

        return new ResponseRegisteredTaskJson
        {
            Id = newTask.Id,
            Name = newTask.Name
        };
    }
}
