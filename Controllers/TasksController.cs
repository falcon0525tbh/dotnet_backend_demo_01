using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TaskHub.Api.Data;
using TaskHub.Api.Models;
using TaskHub.Api.Enums;
using TaskStatusEnum = TaskHub.Api.Enums.TaskStatus;
using TaskPriorityEnum = TaskHub.Api.Enums.TaskPriority;
using TaskHub.Api.Dtos; 
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace TaskHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class TasksController : ControllerBase
{
    private readonly TaskHubDbContext _db;

    public TasksController(TaskHubDbContext db)
    {
        _db = db;
    }

    private Guid GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier)?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if(string.IsNullOrEmpty(id))
        {
            throw  new UnauthorizedAccessException("User ID not found in token");
        }
        return Guid.Parse(id);
    }

    private Role GetUserRole()
    {
        var roleStr = User.FindFirstValue(ClaimTypes.Role);
        if(Enum.TryParse<Role>(roleStr, out var role))
        {
            return role;
        }
        throw new UnauthorizedAccessException("Invalid user role");
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var userId = GetUserId();
        int totalTasks = await _db.Tasks.CountAsync(t => t.AssigneeId == userId);
        int completedTasks = await _db.Tasks.CountAsync(t => t.AssigneeId == userId && t.Status == TaskStatusEnum.Completed);
        int pendingTasks = await _db.Tasks.CountAsync(t => t.AssigneeId == userId && t.Status != TaskStatusEnum.Completed);
        var allTasksList = await _db.Tasks.Where(t => t.AssigneeId == userId).ToListAsync();
        var completedTasksList = allTasksList.Where(t => t.Status == TaskStatusEnum.Completed).ToList();
        var pendingTasksList = allTasksList.Where(t => t.Status != TaskStatusEnum.Completed).ToList();

        return Ok(new { totalTasks, completedTasks, pendingTasks, allTasksList , completedTasksList, pendingTasksList });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool orderByDesc = true,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query =  _db.Tasks.AsQueryable();

        var userRole = GetUserRole();
        if(userRole == Role.CEO)
        {
            
        }
        else if(userRole == Role.Manager)
        {
            var userId = GetUserId();
            var teamMemberIds = await _db.Users
                .Where(u => u.Role == Role.Worker)
                .Select(u => u.Id)
                .ToListAsync();
            teamMemberIds.Add(userId); 
            query = query.Where(t => teamMemberIds.Contains(t.AssigneeId));
        } else if(userRole == Role.Worker)
        {
            var userId = GetUserId();
            query = query.Where(t => t.AssigneeId ==  userId);
        }

        if(orderByDesc){
            query = query.OrderByDescending(t => t.CreatedAt);
        } else {
            query = query.OrderBy(t => t.CreatedAt);
        }
        var list = await query.Select(t => new {
            t.Id,
            t.Title,
            t.Description,
            t.Status,
            t.Priority,
            t.DueDate,
            t.AssigneeId,
            AssigneeName = _db.Users
                .Where(u => u.Id == t.AssigneeId)
                .Select(u => u.UserName)
                .FirstOrDefault(),
            t.CreatedBy,
            CreatedByName = _db.Users
                .Where(u => u.Id == t.CreatedBy)
                .Select(u => u.UserName)
                .FirstOrDefault(),
            t.CreatedAt,
            t.UpdatedAt
        }).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return Ok(new { list, page, pageSize });
    }

    [HttpGet("search")]
    public async Task<IActionResult> Get(
        [FromQuery] Guid? assigneeId,
        [FromQuery] TaskStatusEnum? status,
        [FromQuery] TaskPriorityEnum? priority,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null
    ) 
    {
        var query = _db.Tasks.AsQueryable();

        var userRole = GetUserRole();
        if(userRole == Role.CEO)
        {
            
        }
        else if(userRole == Role.Manager)
        {
            var userId = GetUserId();
            var teamMemberIds = await _db.Users
                .Where(u => u.Role == Role.Worker)
                .Select(u => u.Id)
                .ToListAsync();
            teamMemberIds.Add(userId); 
            query = query.Where(t => teamMemberIds.Contains(t.AssigneeId));
        } else if(userRole == Role.Worker)
        {
            var userId = GetUserId();
            query = query.Where(t => t.AssigneeId ==  userId);
        }

        if (assigneeId.HasValue)
        {
            query = query.Where(t => t.AssigneeId == assigneeId.Value);
        }
        if (status.HasValue)
        {
            query = query.Where(t => t.Status == status.Value);
        }
        if (priority.HasValue)
        {
            query = query.Where(t => t.Priority == priority.Value);
        }
        if (!string.IsNullOrEmpty(search ))
        {
            query = query.Where(t => 
                t.Title.Contains(search ) 
                || (t.Description != null && t.Description.Contains(search)));
        }
        if(startDate.HasValue)
        {
            query = query.Where(t => t.CreatedAt >= startDate.Value);
        }
        if(endDate.HasValue)
        {
            query = query.Where(t => t.CreatedAt <= endDate.Value);
        }
        var list = await query.OrderByDescending(t => t.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(new { list, page, pageSize });
    }

    //get by id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _db.Tasks
            .Where(t => t.Id == id)
            .Select(t => new
            {
                t.Id,
                t.Title,
                t.Description,
                t.Status,
                t.Priority,
                t.DueDate,
                t.AssigneeId,
                AssigneeName = _db.Users
                    .Where(u => u.Id == t.AssigneeId)
                    .Select(u => u.UserName)
                    .FirstOrDefault(),
                t.CreatedBy,
                CreatedByName = _db.Users
                    .Where(u => u.Id == t.CreatedBy)
                    .Select(u => u.UserName)
                    .FirstOrDefault(),
                t.CreatedAt,
                t.UpdatedAt
            })
        .FirstOrDefaultAsync();
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    //create
    [HttpPost("create")]
    public async Task<IActionResult> Create(TaskCreateDto dto)
    {
        if(string.IsNullOrEmpty(dto.Title))
        {
                return BadRequest("Title cannot be empty");
        }
        if(dto.DueDate < DateTime.UtcNow)
        {
                return BadRequest("Due date cannot be in the past");
        }
        var userId = GetUserId();
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            AssigneeId = dto.AssigneeId,
            CreatedBy = userId,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            Status = TaskStatusEnum.New
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
    }

    //update
    [HttpPatch("update/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, TaskUpdateDto dto)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        if (dto.Title != null)
        {
            task.Title = dto.Title;
        }
        if (dto.Description != null)
        {
            task.Description = dto.Description;
        }
        if (dto.Status.HasValue)
        {
            task.Status = dto.Status.Value;
        }
        if (dto.Priority.HasValue)
        {
            task.Priority = dto.Priority.Value;
        }
        if (dto.DueDate.HasValue)
        {
            if(dto.DueDate.Value < DateTime.UtcNow)
            {
                return BadRequest("Due date cannot be in the past");
            }
            task.DueDate = dto.DueDate.Value;
        }
        if (dto.AssigneeId.HasValue)
        {
            task.AssigneeId = dto.AssigneeId.Value;
        }
        task.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return Ok(task);
    }

    //update status
    [HttpPatch("update/{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, TaskStatusUpdateDto dto)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        task.Status = dto.Status;
        task.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(task);
    }

    //delete
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _db.Tasks.Remove(task);
        task.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return Ok(task);
    }

}