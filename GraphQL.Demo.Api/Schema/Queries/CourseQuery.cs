using Bogus;
using GraphQL.Demo.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.Api.Schema.Queries;

public class CourseQuery
{
    private readonly Faker<Course> _courseFaker;
    private readonly Faker<Student> _studentTypeFaker;
    private readonly Faker<Instructor> _instructorTypeFaker;
    private readonly AppDbContext _appDbContext;

    public CourseQuery(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        _instructorTypeFaker = new Faker<Instructor>()
            .RuleFor(f => f.Id, f => Guid.NewGuid())
            .RuleFor(f => f.FirstName, f => f.Name.FirstName())
            .RuleFor(f => f.LastName, f => f.Name.LastName())
            .RuleFor(f => f.Salary, f => f.Random.Double(0, 10000));

        _studentTypeFaker = new Faker<Student>()
            .RuleFor(f => f.Id, f => Guid.NewGuid())
            .RuleFor(f => f.FirstName, f => f.Name.FirstName())
            .RuleFor(f => f.LastName, f => f.Name.LastName())
            .RuleFor(f => f.GPA, f => f.Random.Double(1, 4));

        _courseFaker = new Faker<Course>()
            .RuleFor(f => f.Id, f => Guid.NewGuid())
            .RuleFor(f => f.Name, f => f.Name.FirstName())
            .RuleFor(f => f.Subject, f => f.PickRandom<Subject>())
            .RuleFor(f => f.Instructor, f => _instructorTypeFaker.Generate())
            .RuleFor(f => f.Students, f => _studentTypeFaker.Generate(3));


        _appDbContext.Courses.Add(new Course()
        {
            Id = Guid.NewGuid(),
            Instructor = new Instructor()
            {
                FirstName = "Ömer",
                LastName = "İpek"
            },
            Name = "GraphQL Course",
            Subject = Subject.Science,
            Students = new List<Student>()
            {
                new Student()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Selami",
                    LastName = "Orhan",
                    GPA = 10.0,
                }
            }
        });

        _appDbContext.SaveChanges();
    }

    public IEnumerable<Course> GetCourses()
    {
        // Fake data nuget bogus: https://www.nuget.org/packages/Bogus#readme-body-tab
        return _courseFaker.Generate(5);
    }

    public async Task<Course> GetCourseById(Guid id)
    {
        await Task.Delay(1000);

        var course = _courseFaker.Generate();
        course.Id = id;
        return course;
    }

    [UseFiltering]
    public List<Course> GetCourseByFiltering([Service] AppDbContext appDbContext)
    {
        return appDbContext.Courses.ToList();
    }
}