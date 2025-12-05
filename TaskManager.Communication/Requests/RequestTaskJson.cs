using System.ComponentModel.DataAnnotations;

namespace TaskManager.Communication.Requests;
public class RequestTaskJson
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Priority { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = "pending";
}
