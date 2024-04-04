using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ScheduleService : IScheduleService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public ScheduleService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    
    public async Task<ServiceResponse<ScheduleDTO>> GetScheduleById(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var schedule = await _repository.GetAsync(new ScheduleSpec(id), cancellationToken);
        
        return schedule != null ?
            ServiceResponse<ScheduleDTO>.ForSuccess(schedule) :
            ServiceResponse<ScheduleDTO>.FromError(CommonErrors.ScheduleNotFound);
    }

    public async Task<ServiceResponse> AddSchedule(ScheduleDTO schedule, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(new Schedule
        {
            StartHour = schedule.StartHour,
            EndHour = schedule.EndHour,
            DayOfWeek = schedule.DayOfWeek,
            ClassroomId = schedule.ClassroomId,
            CourseId = schedule.CourseId,
            TeacherId = schedule.TeacherId,
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateSchedule(ScheduleUpdateDTO schedule, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        var existingSchedule = await _repository.GetAsync(new ScheduleUpdateSpec(schedule.Id), cancellationToken);
        
        if (existingSchedule == null)
        {
            return ServiceResponse.FromError(CommonErrors.ScheduleNotFound);
        }
        
        existingSchedule.StartHour = schedule.StartHour ?? existingSchedule.StartHour;
        existingSchedule.EndHour = schedule.EndHour ?? existingSchedule.EndHour;
        existingSchedule.DayOfWeek = schedule.DayOfWeek ?? existingSchedule.DayOfWeek;
        existingSchedule.ClassroomId = schedule.ClassroomId ?? existingSchedule.ClassroomId;
        existingSchedule.CourseId = schedule.CourseId ?? existingSchedule.CourseId;
        existingSchedule.TeacherId = schedule.TeacherId ?? existingSchedule.TeacherId;
        
        await _repository.UpdateAsync(existingSchedule, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteSchedule(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync<Schedule>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse<List<GetScheduleForClassDTO>>> GetSchedulesForClass(Guid classroomId, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var schedules = await _repository.ListAsync(new GetSchedulesForClassSpec(classroomId), cancellationToken);
        return ServiceResponse<List<GetScheduleForClassDTO>>.ForSuccess(schedules);
    }
    
    public async Task<ServiceResponse<List<GetScheduleForTeacherDTO>>> GetSchedulesForTeacher(Guid teacherId, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var schedules = await _repository.ListAsync(new GetScheduleForTeacherSpec(teacherId), cancellationToken);
        return ServiceResponse<List<GetScheduleForTeacherDTO>>.ForSuccess(schedules);
    }
}