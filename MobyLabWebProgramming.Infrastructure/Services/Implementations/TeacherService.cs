using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Data;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class TeacherService : ITeacherService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public TeacherService(IRepository<WebAppDatabaseContext> repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<TeacherDTO>> GetTeacherById(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var teacher = await _repository.GetAsync(new TeacherSpec(id), cancellationToken);
        
        return teacher != null ?
            ServiceResponse<TeacherDTO>.ForSuccess(teacher) :
            ServiceResponse<TeacherDTO>.FromError(CommonErrors.TeacherNotFound);
    }

    public async Task<ServiceResponse<List<TeacherDTO>>> GetTeachers(UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var teachers = await _repository.ListAsync(new GetTeachersSpec(), cancellationToken);
        
        return ServiceResponse<List<TeacherDTO>>.ForSuccess(teachers);
    }

    public async Task<ServiceResponse> AddTeacher(AddTeacherDTO teacher, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only the admin can add a teacher!"));
        }
        
        var newTeacher = new Teacher
        {
            Name = teacher.Name,
            Email = teacher.Email,
        };
        
        await _repository.AddAsync(newTeacher, cancellationToken);
        
        foreach (Guid courseId in teacher.CourseIds)
        {
            _repository.DbContext.TeacherCourses.Add(new TeacherCourses
            {
                TeacherID = newTeacher.Id,
                CourseID = courseId,
            });
        }
        await _repository.DbContext.SaveChangesAsync(cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> UpdateTeacher(TeacherUpdateDTO teacher, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != teacher.Id)
        {
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only the admin or the teacher can update a teacher!"));
        }
        
        var existingTeacher = await _repository.GetAsync(new TeacherUpdateSpec(teacher.Id), cancellationToken);
        
        if (existingTeacher == null)
        {
            return ServiceResponse.FromError(CommonErrors.TeacherNotFound);
        }
        
        existingTeacher.Name = teacher.Name ?? existingTeacher.Name;
        existingTeacher.Email = teacher.Email ?? existingTeacher.Email;
        
        await _repository.UpdateAsync(existingTeacher, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteTeacher(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only the admin can delete a teacher!"));
        }
        await _repository.DeleteAsync<Teacher>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse<List<GetTeachersByCourseDTO>>> GetTeachersByCourse(Guid courseId, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var teachers = await _repository.ListAsync(new GetTeachersByCourseSpec(courseId), cancellationToken);
        
        return ServiceResponse<List<GetTeachersByCourseDTO>>.ForSuccess(teachers);
    }
    
    public async Task<ServiceResponse<List<GetTeacherScheduleByEmailDTO>>> GetTeacherSchedule(UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Teacher)
        {
            return ServiceResponse<List<GetTeacherScheduleByEmailDTO>>.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only the teacher can get his schedule!"));
        }
        var email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
        var schedules = await _repository.ListAsync(new GetTeacherScheduleByEmailSpec(email), cancellationToken);
        
        return ServiceResponse<List<GetTeacherScheduleByEmailDTO>>.ForSuccess(schedules);
    }
}