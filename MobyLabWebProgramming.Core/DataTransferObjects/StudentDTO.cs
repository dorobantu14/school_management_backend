namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class StudentDTO
{
    public String Name { get; set; } = default!;
    public String Email { get; set; } = default!;
    public Guid ClassroomId { get; set; }
}