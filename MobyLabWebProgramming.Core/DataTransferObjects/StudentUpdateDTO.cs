namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record StudentUpdateDTO(Guid Id, string? Name = default, string? Email = default, Guid? ClassroomId = default);