namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ScheduleDTO
{
    public String DayOfWeek { get; set; } = default!;
    public int StartHour { get; set; } = default!;
    public int EndHour { get; set; } = default!;
    public Guid ClassroomId { get; set; }
    public Guid CourseId { get; set; }
    
    public Guid TeacherId { get; set; }
}