
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskHub.Api.Data;

public class TaskHubDbContextFactory : IDesignTimeDbContextFactory<TaskHubDbContext>
{
    public TaskHubDbContext CreateDbContext(string[] args)
    {
        var config =  new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.Development.json", optional:false)
            .Build();

        var conn = config.GetConnectionString("Default");

        var options = new DbContextOptionsBuilder<TaskHubDbContext>()
            .UseSqlServer(conn)
            .Options;

            return new TaskHubDbContext(options);
    }
}