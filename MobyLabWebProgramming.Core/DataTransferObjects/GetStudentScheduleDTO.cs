namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class GetStudentScheduleDTO
{
    public String DayOfWeek { get; set; } = default!;
    public int StartHour { get; set; } = default!;
    public int EndHour { get; set; } = default!;
    public CourseDTO Course { get; set; } = default!;
}