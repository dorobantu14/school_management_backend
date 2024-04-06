using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AddTeacherDTO
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<Guid> CourseIds { get; set; } = new List<Guid>();
}