namespace MobyLabWebProgramming.Core.Entities;

public class Student : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Guid ClassroomId { get; set; }
    public Classroom Classroom { get; set; } = default!;
}