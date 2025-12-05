using TaskManager.Application.Exceptions;
using TaskManager.Application.AppServices;
using TaskManager.Communication.Responses;

namespace TaskManager.Application.UseCases.Tasks.GetById;
public class GetTaskByIdUseCase
{

    public ResponseTaskJson Execute(Guid id)
    {
        var task = TaskAppService.GetById(id);
        
        if (task == null)
        {
            throw new NotFoundException($"Task with ID {id} not found");
        }

        return new ResponseTaskJson
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Priority = task.Priority,
            DueDate = task.DueDate,
            Status = task.Status
        };
    }
}
