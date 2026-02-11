using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskHub.Api.Enums;
using TaskStatusEnum = TaskHub.Api.Enums.TaskStatus;
using TaskPriorityEnum = TaskHub.Api.Enums.TaskPriority;
using TaskHub.Api.Models;

namespace TaskHub.Api.Data;

public class TaskHubDbContext : DbContext
{
    public TaskHubDbContext(DbContextOptions<TaskHubDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            /*
            fixed GUID format easier when delete
                DELETE FROM [Users]
                WHERE CONVERT(varchar(36), Id) LIKE '00000000-%';
            */
            new User
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), //fixed GUID format easier when delete
                UserName = "ChongHsien",
                Role = Role.CEO,
                PasswordHash = "ChongHsien",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), //fixed GUID format easier when delete
                UserName = "YeeChian",
                Role = Role.Manager,
                PasswordHash = "YeeChian",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), //fixed GUID format easier when delete
                UserName = "Falcon",
                Role = Role.Worker,
                PasswordHash = "Falcon",
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<TaskItem>().HasData(
            /*
            fixed GUID format easier when delete
                DELETE FROM [Tasks]
                WHERE CONVERT(varchar(36), Id) LIKE '00000000-%';
            */
            new TaskItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Title = "ChongHsien Task",
                Description = "ChongHsien Task Description",
                DueDate = DateTime.UtcNow.AddDays(7),
                Status = TaskStatusEnum.New,
                Priority = TaskPriorityEnum.High,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                AssigneeId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TaskItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Title = "YeeChian Task",
                Description = "YeeChian Task Description",
                DueDate = DateTime.UtcNow.AddDays(8),
                Status = TaskStatusEnum.InProgress,
                Priority = TaskPriorityEnum.Medium,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                AssigneeId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TaskItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Title = "Falcon Task",
                Description = "Falcon Task Description",
                DueDate = DateTime.UtcNow.AddDays(9),
                Status = TaskStatusEnum.UAT,
                Priority = TaskPriorityEnum.Low,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                AssigneeId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            
            new TaskItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Title = "ChongHsien to Falcon Task",
                Description = "ChongHsien toFalcon Task Description",
                DueDate = DateTime.UtcNow.AddDays(10),
                Status = TaskStatusEnum.Closed,
                Priority = TaskPriorityEnum.High,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                AssigneeId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            
            new TaskItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Title = "YeeChian to Falcon Task",
                Description = "YeeChian Task Description",
                DueDate = DateTime.UtcNow.AddDays(11),
                Status = TaskStatusEnum.Completed,
                Priority = TaskPriorityEnum.Low,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                AssigneeId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            }
        );
    }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<User> Users => Set<User>();
}
