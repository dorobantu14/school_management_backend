using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IStudentService
{
    public Task<ServiceResponse<StudentDTO>> GetStudentById(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<List<GetStudentsByClassDTO>>> GetStudentByClassroomId(Guid classroomId, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddStudent(StudentDTO student, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateStudent(StudentUpdateDTO student, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteStudent(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<List<GetStudentScheduleDTO>>> GetStudentSchedule(
        UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}