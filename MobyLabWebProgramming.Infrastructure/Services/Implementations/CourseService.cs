using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public CourseService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    
    public async Task<ServiceResponse<CourseDTO>> GetCourseById(Guid id, CancellationToken cancellationToken = default)
    {
        var course = await _repository.GetAsync(new CourseSpec(id), cancellationToken);
        
        return course == null ? ServiceResponse<CourseDTO>.FromError(CommonErrors.CourseNotFound) : ServiceResponse<CourseDTO>.ForSuccess(course);
    }

    public async Task<ServiceResponse> AddCourse(CourseDTO course, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only the admin can add a course!"));
        }
        await _repository.AddAsync(new Course
        {
            Name = course.Name,
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateCourse(CourseUpdateDTO course, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only the admin can update a course!"));
        }
        var existingCourse = await _repository.GetAsync(new CourseUpdateSpec(course.Id), cancellationToken);
        
        if (existingCourse == null)
        {
            return ServiceResponse.FromError(CommonErrors.CourseNotFound);
        }
        
        existingCourse.Name = course.Name ?? existingCourse.Name;
        
        await _repository.UpdateAsync(existingCourse, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCourse(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync<Course>(id, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    public async Task<ServiceResponse<List<GetSchedulesForCourseDTO>>> GetSchedulesForCourse(Guid courseId, CancellationToken cancellationToken = default)
    {
        var schedules = await _repository.ListAsync(new GetScheduleForCourseSpec(courseId), cancellationToken);
        
        return ServiceResponse<List<GetSchedulesForCourseDTO>>.ForSuccess(schedules);
    }
}