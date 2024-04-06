namespace MobyLabWebProgramming.Core.Entities;

public class Course : BaseEntity
{
    public String Name { get; set; } = default!;
    // public List<Teacher> Teachers { get; set; } = default!;
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    public ICollection<TeacherCourses> TeacherCourses { get; set; } = new List<TeacherCourses>();
}