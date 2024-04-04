using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IScheduleService
{
    public Task<ServiceResponse<ScheduleDTO>> GetScheduleById(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddSchedule(ScheduleDTO schedule, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateSchedule(ScheduleUpdateDTO schedule, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteSchedule(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    // Get all schedules for a class.
    public Task<ServiceResponse<List<GetScheduleForClassDTO>>> GetSchedulesForClass(Guid classId, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    // Get all schedules for a teacher.
    public Task<ServiceResponse<List<GetScheduleForTeacherDTO>>> GetSchedulesForTeacher(Guid teacherId, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}