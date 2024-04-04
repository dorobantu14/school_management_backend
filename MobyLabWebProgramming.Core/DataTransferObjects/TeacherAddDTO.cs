namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TeacherAddDTO
{
    public int TeacherId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Email { get; set; } = default!;
}