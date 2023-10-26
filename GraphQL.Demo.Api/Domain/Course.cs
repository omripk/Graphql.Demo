namespace GraphQL.Demo.Api.Domain;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    
    [GraphQLNonNullType]
    public Instructor Instructor { get; set; }
    
    public IEnumerable<Student> Students { get; set; }
}

public enum Subject
{
    Mathematics,
    Science,
    History
}