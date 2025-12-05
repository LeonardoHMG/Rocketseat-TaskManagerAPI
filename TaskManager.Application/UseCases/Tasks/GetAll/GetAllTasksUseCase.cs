using TaskManager.Application.AppServices;
using TaskManager.Communication.Responses;

namespace TaskManager.Application.UseCases.Tasks.GetAll;
public class GetAllTasksUseCase
{
    public ResponseAllTaskJson Execute()
    {
        var tasks = TaskAppService.GetAll();

        return new ResponseAllTaskJson
        {
            Tasks = tasks.Select(t => new ResponseShortTaskJson
            {
                Id = t.Id,
                Name = t.Name,
                Priority = t.Priority,
                Status = t.Status
            }).ToList()
        };
    }
}
