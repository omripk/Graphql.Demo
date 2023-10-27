using GraphQL.Demo.Api.Domain;

namespace GraphQL.Demo.Api.Schema.Queries;

public class CourseFilteringType
{
    public Guid Id { get; set; }
    public Subject Subject { get; set; }
    public string Name { get; set; }
}