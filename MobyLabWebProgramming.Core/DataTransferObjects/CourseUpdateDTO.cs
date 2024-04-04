namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record CourseUpdateDTO(Guid Id, string? Name = default!);