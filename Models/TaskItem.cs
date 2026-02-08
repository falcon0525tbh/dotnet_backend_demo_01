using TaskHub.Api.Enums;
using TaskStatusEnum = TaskHub.Api.Enums.TaskStatus;
using TaskPriorityEnum = TaskHub.Api.Enums.TaskPriority;

namespace TaskHub.Api.Models;


public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.New;
    public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.Medium;
    public DateTime DueDate { get; set; } 
    public Guid AssigneeId { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
