namespace MobyLabWebProgramming.Core.Entities;

public class Teacher : BaseEntity
{
    public string? Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Guid CourseId { get; set; }
    public List<Course> Courses { get; set; } = default!;
}