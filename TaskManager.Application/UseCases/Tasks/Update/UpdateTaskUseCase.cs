using TaskManager.Application.Exceptions;
using TaskManager.Application.AppServices;
using TaskManager.Application.Validation;
using TaskManager.Communication.Requests;

namespace TaskManager.Application.UseCases.Tasks.Update;
public class UpdateTaskUseCase
{
    public void Execute(Guid id, RequestTaskJson request)
    {
        TaskValidator.Validate(request);

        var existingTask = TaskAppService.GetById(id);

        if (existingTask == null)
        {
            throw new NotFoundException($"Task with ID {id} not found.");
        }

        if (TaskAppService.Exists(request.Name, id))
        {
            throw new ConflictException($"A task with name '{request.Name}' already exists.");
        }

        existingTask.Name = request.Name.Trim();
        existingTask.Description = request.Description?.Trim() ?? string.Empty;
        existingTask.Priority = request.Priority.Trim();
        existingTask.DueDate = request.DueDate;
        existingTask.Status = request.Status.Trim();

        TaskAppService.Update(existingTask);
    }
}
