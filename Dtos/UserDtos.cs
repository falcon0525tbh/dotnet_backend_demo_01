using TaskHub.Api.Enums;

namespace TaskHub.Api.Dtos;

public record UserCreateDto(
    string UserName,
    string Password,
    int Role
);

public record UserUpdateDto(
    Guid Id,
    string? UserName,
    string? Password,
    int? Role,
    bool? Active
);

public record UserResponse(Guid Id, string UserName, int Role, DateTime CreatedAt, bool Active);