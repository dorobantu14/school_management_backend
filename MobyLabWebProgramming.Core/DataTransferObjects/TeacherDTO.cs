using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TeacherDTO
{ 
    public string Name { get; set; } = default!;
    public Guid CourseId { get; set; }
    public string Email { get; set; } = default!;
}