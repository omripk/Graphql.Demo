using GraphQL.Demo.Api.Domain;
using GraphQL.Demo.Api.Schema.Queries;
using GraphQL.Demo.Api.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQL.Demo.Api.Schema.Mutations;

public class CourseMutation
{
    private readonly List<CourseResult> _courses;

    public CourseMutation()
    {
        _courses = new List<CourseResult>()
        {
            new CourseResult()
            {
                Id = Guid.Parse("d3538480-1759-4b6b-9962-c5aba49d83d3"),
                Name = "Test",
                Subject = Subject.Mathematics,
                InstructorId = Guid.NewGuid()
            }
        };
    }

    public CourseResult CreateCourse(string name, Subject subject, Guid instructorId)
    {
        var course = new CourseResult()
        {
            Id = Guid.NewGuid(),
            InstructorId = instructorId,
            Name = name,
            Subject = subject
        };
        _courses.Add(course);

        return course;
    }

    public async Task<CourseResult> CreateCourseV2(CreateOrUpdateCourseInputType createOrUpdateCourseInputType,
        [Service] ITopicEventSender topicEventSender)
    {
        var course = new CourseResult()
        {
            Id = Guid.NewGuid(),
            InstructorId = createOrUpdateCourseInputType.InstructorId,
            Name = createOrUpdateCourseInputType.Name,
            Subject = createOrUpdateCourseInputType.Subject
        };
        _courses.Add(course);

        await topicEventSender.SendAsync(nameof(CourseSubscription.CourseCreated), course);

        return course;
    }

    public async Task<CourseResult> UpdateCourse(Guid id, string name, Subject subject, Guid instructorId,
        [Service] ITopicEventSender topicEventSender)
    {
        var course = _courses.FirstOrDefault(x => x.Id == id);
        if (course is null)
        {
            throw new GraphQLException(new Error("Course not found", "001"));
        }

        course.Name = name;
        course.Subject = subject;
        course.InstructorId = instructorId;

        string updateCourseTopic = $"{course.Id}_{nameof(CourseSubscription.CourseUpdated)}";
        await topicEventSender.SendAsync(updateCourseTopic, course);

        return course;
    }

    public bool DeleteCourse(Guid id)
    {
        var course = _courses.FirstOrDefault(x => x.Id == id);
        if (course is null)
        {
            throw new GraphQLException(new Error("Course not found", "001"));
        }

        return _courses.Remove(course);
    }
}