using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IClassroomService
{
    public Task<ServiceResponse<ClassroomDTO>> GetClassroomById(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddClassroom(ClassroomDTO classroom, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateClassroom(ClassroomUpdateDTO classroom, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteClassroom(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}