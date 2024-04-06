
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITeacherService
{
    public Task<ServiceResponse<TeacherDTO>> GetTeacherById(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddTeacher(AddTeacherDTO teacher, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> UpdateTeacher(TeacherUpdateDTO teacher, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> DeleteTeacher(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<List<GetTeachersByCourseDTO>>> GetTeachersByCourse(Guid courseId, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<List<TeacherDTO>>> GetTeachers(UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    Task<ServiceResponse<List<GetTeacherScheduleByEmailDTO>>> GetTeacherSchedule(UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}