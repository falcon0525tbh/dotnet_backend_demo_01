using TaskHub.Api.Enums;
namespace TaskHub.Api.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Role Role { get; set; } = Role.Worker;
}
