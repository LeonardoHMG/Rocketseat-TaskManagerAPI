using TaskManager.Application.Exceptions;
using TaskManager.Application.AppServices;

namespace TaskManager.Application.UseCases.Tasks.Delete;
public class DeleteTaskByIdUseCase
{
    public void Execute(Guid id)
    {
        var task = TaskAppService.GetById(id);

        if (task != null)
        {
            TaskAppService.Delete(id);
        }
        else
        {
            throw new NotFoundException($"Task with ID {id} not found");
        }
    }

}
