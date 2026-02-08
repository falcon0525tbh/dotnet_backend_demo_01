using TaskHub.Api.Enums;
using TaskStatusEnum = TaskHub.Api.Enums.TaskStatus;
using TaskPriorityEnum = TaskHub.Api.Enums.TaskPriority;

namespace TaskHub.Api.Dtos;

public record TaskCreateDto(
    string Title,
    string? Description,
    TaskPriorityEnum Priority,
    DateTime DueDate,
    Guid AssigneeId
);

public record TaskUpdateDto(
    string? Title,
    string? Description,
    TaskPriorityEnum? Priority,
    DateTime? DueDate,
    Guid? AssigneeId,
    TaskStatusEnum? Status
);


public record TaskStatusUpdateDto(TaskStatusEnum Status);
public record TaskReAssignDto(Guid AssigneeId); 