using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class TeacherService : ITeacherService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public TeacherService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<TeacherDTO>> GetTeacherById(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var teacher = await _repository.GetAsync(new TeacherSpec(id), cancellationToken);
        
        return teacher != null ?
            ServiceResponse<TeacherDTO>.ForSuccess(teacher) :
            ServiceResponse<TeacherDTO>.FromError(CommonErrors.TeacherNotFound);
    }

    public async Task<ServiceResponse> AddTeacher(TeacherDTO teacher, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(new Teacher
        {
            Name = teacher.Name,
            CourseId = teacher.CourseId,
            Email = teacher.Email,
          
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> UpdateTeacher(TeacherUpdateDTO teacher, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        var existingTeacher = await _repository.GetAsync(new TeacherUpdateSpec(teacher.Id), cancellationToken);
        
        if (existingTeacher == null)
        {
            return ServiceResponse.FromError(CommonErrors.TeacherNotFound);
        }
        
        existingTeacher.Name = teacher.Name ?? existingTeacher.Name;
        existingTeacher.CourseId = teacher.CourseId ?? existingTeacher.CourseId;
        existingTeacher.Email = teacher.Email ?? existingTeacher.Email;
        
        await _repository.UpdateAsync(existingTeacher, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteTeacher(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync<Teacher>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse<List<GetTeachersByCourseDTO>>> GetTeachersByCourse(Guid courseId, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var teachers = await _repository.ListAsync(new GetTeachersByCourseSpec(courseId), cancellationToken);
        
        return ServiceResponse<List<GetTeachersByCourseDTO>>.ForSuccess(teachers);
    }
}