using Bogus;
using GraphQL.Demo.Api.Domain;

namespace GraphQL.Demo.Api.Schema;

public class Query
{
    private readonly Faker<CourseType> _courseFaker;
    private readonly Faker<StudentType> _studentTypeFaker;
    private readonly Faker<InstructorType> _instructorTypeFaker;

    public Query()
    {
        _instructorTypeFaker = new Faker<InstructorType>()
            .RuleFor(f => f.Id, f => Guid.NewGuid())
            .RuleFor(f => f.FirstName, f => f.Name.FirstName())
            .RuleFor(f => f.LastName, f => f.Name.LastName())
            .RuleFor(f => f.Salary, f => f.Random.Double(0, 10000));

        _studentTypeFaker = new Faker<StudentType>()
            .RuleFor(f => f.Id, f => Guid.NewGuid())
            .RuleFor(f => f.FirstName, f => f.Name.FirstName())
            .RuleFor(f => f.LastName, f => f.Name.LastName())
            .RuleFor(f => f.GPA, f => f.Random.Double(1, 4));

        _courseFaker = new Faker<CourseType>()
            .RuleFor(f => f.Id, f => Guid.NewGuid())
            .RuleFor(f => f.Name, f => f.Name.FirstName())
            .RuleFor(f => f.Subject, f => f.PickRandom<Subject>())
            .RuleFor(f => f.Instructor, f => _instructorTypeFaker.Generate())
            .RuleFor(f => f.Students, f => _studentTypeFaker.Generate(3));
    }

    public IEnumerable<CourseType> GetCourses()
    {
        // Fake data nuget bogus: https://www.nuget.org/packages/Bogus#readme-body-tab
        return _courseFaker.Generate(5);
    }

    public async Task<CourseType> GetCourseById(Guid id)
    {
        await Task.Delay(1000);

        var course = _courseFaker.Generate();
        course.Id = id;
        return course;
    }

    [GraphQLDeprecated("this is deprecated")]
    public string Instructions => "test";
}