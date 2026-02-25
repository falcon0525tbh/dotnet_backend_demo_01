using TaskPriorityEnum = TaskHub.Api.Enums.TaskPriority;
using System;
using System.Collections.Generic;

namespace TaskHub.Api.Dtos;

public record ProjectCreateDto(
    string Name,
    string? Description,
    DateTime? DueDate,
    TaskPriorityEnum? Priority,
    List<Guid> MemberIds
);
public record ProjectUpdateDto(
    Guid Id,
    string? Name,
    string? Description,
    DateTime? DueDate,
    TaskPriorityEnum? Priority,
    bool? Active,
    List<Guid>? MemberIds
);
public record ProjectResponse(
    Guid Id,
    string Name,
    string? Description,
    DateTime? DueDate,
    TaskPriorityEnum? Priority,
    bool Active,
    Guid CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<Guid> MemberIds,
    List<ProjectMemberDto> Members
);

public record ProjectMemberDto(Guid Id, string UserName);
