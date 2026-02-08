using TaskHub.Api.Enums;

namespace TaskHub.Api.Contracts;

public record LoginRequest(string UserName, string Password);
public record LoginResponse(
    string Token,
    Guid UserId,
    Role Role
);