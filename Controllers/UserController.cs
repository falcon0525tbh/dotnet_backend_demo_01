using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TaskHub.Api.Data;
using TaskHub.Api.Models;
using TaskHub.Api.Enums;
using TaskHub.Api.Dtos; 
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace TaskHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class UserController : ControllerBase
{
    private readonly TaskHubDbContext _db;

    public UserController(TaskHubDbContext db)
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

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        
        var query = _db.Users.AsQueryable();

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
            query = query.Where(t => teamMemberIds.Contains(t.Id));
        } else if(userRole == Role.Worker)
        {
            var userId = GetUserId();
            query = query.Where(t => t.Id ==  userId);
        }
        
        var users = await query
            .Select(u => new 
            {
                u.Id,
                u.UserName,
                u.CreatedAt,
                u.Role
            })
            .ToListAsync();

        return Ok(users);
    }
}