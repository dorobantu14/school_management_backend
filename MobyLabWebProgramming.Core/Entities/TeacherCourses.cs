namespace MobyLabWebProgramming.Core.Entities;

public class TeacherCourses : BaseEntity
{
    public Guid TeacherID { get; set; }
    public Teacher Teacher { get; set; } = default!;
    public Guid CourseID { get; set; }
    public Course Course { get; set; } = default!;
}