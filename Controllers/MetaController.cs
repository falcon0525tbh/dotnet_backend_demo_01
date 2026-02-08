using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TaskHub.Api.Data;
using TaskHub.Api.Models;
using TaskHub.Api.Enums;
using TaskPriority = TaskHub.Api.Enums.TaskPriority;
using TaskStatus = TaskHub.Api.Enums.TaskStatus;
using TaskHub.Api.Dtos; 
using System.Security.Claims;
namespace TaskHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MetaController : ControllerBase
{
    private readonly TaskHubDbContext _db;

    public MetaController(TaskHubDbContext db)
    {
        _db = db;
    }

    [HttpGet("enums")]
    public IActionResult GetEnums()
    {
        var result = new
        {
            TaskPriorities = Enum.GetValues(typeof(TaskPriority)).Cast<TaskPriority>().Select(tp => tp.ToString()).ToList(),
            TaskStatuses = Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>().Select(ts => ts.ToString()).ToList(),
            Role = Enum.GetValues(typeof(Role)).Cast<Role>().Select(r => r.ToString()).ToList()
        };
        return Ok(result);
    }
}