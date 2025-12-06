using System.Xml.Linq;
using TaskManager.Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;

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

    public static bool Exists(string name, Guid? exceptId = null)
    {
        var normalizedName = name.Trim().ToLower();

        return tasks.Any(t =>
            t.Name.Trim().ToLower() == normalizedName &&
            (exceptId == null || t.Id != exceptId)
        );
    }

}
