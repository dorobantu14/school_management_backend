using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TeacherDTO
{ 
    public string Name { get; set; } = default!;
    public List<Course> Courses { get; set; } = default!;
    public string Email { get; set; } = default!;
}