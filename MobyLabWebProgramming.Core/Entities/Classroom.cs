namespace MobyLabWebProgramming.Core.Entities;

public class Classroom : BaseEntity
{
    public String Name { get; set; } = default!;
    public List<Student> Students { get; set; } = default!;
    public List<Schedule> Schedules { get; set; } = default!;
}