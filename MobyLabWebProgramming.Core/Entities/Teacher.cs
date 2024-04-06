namespace MobyLabWebProgramming.Core.Entities;

public class Teacher : BaseEntity
{
    public string? Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    
    // public List<Guid> CourseIds { get; set; } = new List<Guid>();
    public ICollection<TeacherCourses> TeacherCourses { get; set; } = new List<TeacherCourses>();
}