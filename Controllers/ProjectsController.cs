using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskHub.Api.Data;
using TaskHub.Api.Dtos;
using TaskHub.Api.Models;
using TaskHub.Api.Enums;
using TaskPriorityEnum = TaskHub.Api.Enums.TaskPriority;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TaskHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly TaskHubDbContext _db;
    public ProjectsController(TaskHubDbContext db) => _db = db;

    private Guid GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(id)) throw new UnauthorizedAccessException("User ID not found in token");
        return Guid.Parse(id);
    }

    private Role GetUserRole()
    {
        var roleStr = User.FindFirstValue(ClaimTypes.Role);
        if (Enum.TryParse<Role>(roleStr, out var role)) return role;
        throw new UnauthorizedAccessException("Invalid user role");
    }

    // GET /api/Projects
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var userRole = GetUserRole();

        var query = _db.Projects.AsQueryable();

        // Only show projects where current user is a member or creator (Admin sees all)
        if (userRole != Role.Admin)
        {
            query = query.Where(p =>
                p.CreatedBy == userId ||
                _db.ProjectUsers.Any(pu => pu.ProjectId == p.Id && pu.UserId == userId));
        }

        return Ok(await query
            .Select(p => new ProjectResponse(
                p.Id,
                p.Name,
                p.Description,
                p.DueDate,
                p.Priority,
                p.Active,
                p.CreatedBy,
                p.CreatedAt,
                p.UpdatedAt,
                _db.ProjectUsers.Where(pu => pu.ProjectId == p.Id).Select(pu => pu.UserId).ToList(),
                _db.ProjectUsers.Where(pu => pu.ProjectId == p.Id)
                    .Join(_db.Users, pu => pu.UserId, u => u.Id, (pu, u) => new ProjectMemberDto(u.Id, u.UserName))
                    .ToList()
            ))
            .ToListAsync());
    }

    // GET /api/Projects/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var p = await _db.Projects.FindAsync(id);
        if (p == null) return NotFound();
        var userRole = GetUserRole();
        if (userRole != Role.Admin)
        {
            var userId = GetUserId();
            var isMember = await _db.ProjectUsers.AnyAsync(pu => pu.ProjectId == id && pu.UserId == userId);
            var isCreator = p.CreatedBy == userId;
            if (!isMember && !isCreator) return Forbid();
        }
        var memberIds = await _db.ProjectUsers.Where(pu => pu.ProjectId == id).Select(pu => pu.UserId).ToListAsync();
        var members = await _db.ProjectUsers.Where(pu => pu.ProjectId == id)
            .Join(_db.Users, pu => pu.UserId, u => u.Id, (pu, u) => new ProjectMemberDto(u.Id, u.UserName))
            .ToListAsync();
        return Ok(new ProjectResponse(p.Id, p.Name, p.Description, p.DueDate, p.Priority, p.Active, p.CreatedBy, p.CreatedAt, p.UpdatedAt, memberIds, members));
    }

    // POST /api/Projects/create
    [HttpPost("create")]
    [Authorize(Roles = "Admin,Manager,CEO")]
    public async Task<IActionResult> Create(ProjectCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest("Name is required");

        var userId = GetUserId();
        var p = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Priority = dto.Priority ?? TaskPriorityEnum.Medium,
            CreatedBy = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Projects.Add(p);
        await _db.SaveChangesAsync();

        if (dto.MemberIds?.Any() == true)
        {
            var links = dto.MemberIds.Distinct().Select(uid => new ProjectUser { ProjectId = p.Id, UserId = uid });
            _db.ProjectUsers.AddRange(links);
            await _db.SaveChangesAsync();
        }

        var memberIds = await _db.ProjectUsers.Where(pu => pu.ProjectId == p.Id).Select(pu => pu.UserId).ToListAsync();
        var members = await _db.ProjectUsers.Where(pu => pu.ProjectId == p.Id)
            .Join(_db.Users, pu => pu.UserId, u => u.Id, (pu, u) => new ProjectMemberDto(u.Id, u.UserName))
            .ToListAsync();
        return Ok(new ProjectResponse(p.Id, p.Name, p.Description, p.DueDate, p.Priority, p.Active, p.CreatedBy, p.CreatedAt, p.UpdatedAt, memberIds, members));
    }

    // PATCH /api/Projects/update/{id}
    [HttpPatch("update/{id:guid}")]
    [Authorize(Roles = "Admin,Manager,CEO")]
    public async Task<IActionResult> Update(Guid id, ProjectUpdateDto dto)
    {
        var p = await _db.Projects.FindAsync(id);
        if (p == null) return NotFound();

        var userId = GetUserId();
        var userRole = GetUserRole();
        var isAdmin = userRole == Role.Admin;
        if (!isAdmin && p.CreatedBy != userId)
        {
            return Forbid();
        }

        if (!string.IsNullOrWhiteSpace(dto.Name)) p.Name = dto.Name;
        if (dto.Description != null) p.Description = dto.Description;
        if (dto.DueDate.HasValue) p.DueDate = dto.DueDate;
        if (dto.Priority.HasValue) p.Priority = dto.Priority.Value;
        if (dto.Active.HasValue) p.Active = dto.Active.Value;

        if (dto.MemberIds != null)
        {
            var existing = _db.ProjectUsers.Where(x => x.ProjectId == id);
            _db.ProjectUsers.RemoveRange(existing);
            if (dto.MemberIds.Any())
            {
                var links = dto.MemberIds.Distinct().Select(uid => new ProjectUser { ProjectId = id, UserId = uid });
                _db.ProjectUsers.AddRange(links);
            }
        }


        p.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        var memberIds = await _db.ProjectUsers.Where(pu => pu.ProjectId == p.Id).Select(pu => pu.UserId).ToListAsync();
        var members = await _db.ProjectUsers.Where(pu => pu.ProjectId == p.Id)
            .Join(_db.Users, pu => pu.UserId, u => u.Id, (pu, u) => new ProjectMemberDto(u.Id, u.UserName))
            .ToListAsync();
        return Ok(new ProjectResponse(p.Id, p.Name, p.Description, p.DueDate, p.Priority, p.Active, p.CreatedBy, p.CreatedAt, p.UpdatedAt, memberIds, members));
    }

    // DELETE /api/Projects/{id}
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,Manager,CEO")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var p = await _db.Projects.FindAsync(id);
        if (p == null) return NotFound();

        _db.Projects.Remove(p);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
