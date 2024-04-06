namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class GetSchedulesForCourseDTO
{
    public String DayOfWeek { get; set; } = default!;
    public int StartHour { get; set; } = default!;
    public int EndHour { get; set; } = default!;
    public String ClassroomName { get; set; } = default!;
    public String TeacherName { get; set; } = default!;
}