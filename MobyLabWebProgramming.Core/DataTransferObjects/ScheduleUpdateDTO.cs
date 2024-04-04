namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record ScheduleUpdateDTO(Guid Id, string? DayOfWeek = default, int? StartHour = default, int? EndHour = default, Guid? ClassroomId = default, Guid? CourseId = default, Guid? TeacherId = default);