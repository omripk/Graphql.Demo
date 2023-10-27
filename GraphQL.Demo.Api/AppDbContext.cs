using GraphQL.Demo.Api.Domain;

namespace GraphQL.Demo.Api;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     
    //     // var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    //     // optionsBuilder.UseInMemoryDatabase(databaseName: “YourInMemoryDatabase”)
    //     //     .UseLoggerFactory(loggerFactory)
    //     //     .EnableSensitiveDataLogging();
    //     optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
    // }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
}