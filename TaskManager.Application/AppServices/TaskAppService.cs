using TaskManager.Domain.Entities;

namespace TaskManager.Application.AppServices;
public static class TaskAppService
{
    private static readonly List<TaskEntity> tasks = [];

    public static List<TaskEntity> GetAll()
    {
        return tasks.ToList();
    }

    public static void Create(TaskEntity entity)
    {
        tasks.Add(entity);
    }

    public static TaskEntity? GetById(Guid id)
    {
        return tasks.FirstOrDefault(t => t.Id == id);
    }

    public static void Update(TaskEntity entity)
    {
        var idx = tasks.FindIndex(t => t.Id == entity.Id);
        if (idx >= 0)
        {
            tasks[idx].Name = entity.Name;
            tasks[idx].Description = entity.Description;
            tasks[idx].Priority = entity.Priority;
            tasks[idx].DueDate = entity.DueDate;
            tasks[idx].Status = entity.Status;
        }
    }

    public static void Delete(Guid id)
    {
        var item = tasks.FirstOrDefault(t => t.Id == id);
        if (item != null) tasks.Remove(item);
    }
}
