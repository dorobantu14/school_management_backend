using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
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

    public async Task<ServiceResponse> AddCourse(CourseDTO course, CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(new Course
        {
            Name = course.Name,
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateCourse(CourseUpdateDTO course, CancellationToken cancellationToken = default)
    {
        var existingCourse = await _repository.GetAsync(new CourseUpdateSpec(course.Id), cancellationToken);
        
        if (existingCourse == null)
        {
            return ServiceResponse.FromError(CommonErrors.CourseNotFound);
        }
        
        existingCourse.Name = course.Name ?? existingCourse.Name;
        
        await _repository.UpdateAsync(existingCourse, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCourse(Guid id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync<Course>(id, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
}