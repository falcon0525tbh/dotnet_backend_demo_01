using System;

namespace TaskHub.Api.Models;

public class ProjectUser
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public Project? Project { get; set; }
    public User? User { get; set; }
};