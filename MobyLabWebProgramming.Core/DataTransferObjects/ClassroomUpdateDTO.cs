using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record ClassroomUpdateDTO(Guid Id, string? Name = default);