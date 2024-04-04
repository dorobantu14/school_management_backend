namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record TeacherUpdateDTO(Guid Id, string? Name = default, string? Email = default, Guid? CourseId = default);