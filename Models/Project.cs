using TaskPriorityEnum = TaskHub.Api.Enums.TaskPriority;

namespace TaskHub.Api.Models;

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.Medium;
    public bool Active { get; set; } = true;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<ProjectUser> ProjectUsers { get; set; } = new();
}