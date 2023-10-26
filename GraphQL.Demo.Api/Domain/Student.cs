namespace GraphQL.Demo.Api.Domain;

public class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [GraphQLName("gpa")]
    public double GPA { get; set; }
}