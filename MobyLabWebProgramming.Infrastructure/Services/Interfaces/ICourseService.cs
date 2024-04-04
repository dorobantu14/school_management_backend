using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICourseService
{
    public Task<ServiceResponse<CourseDTO>> GetCourseById(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddCourse(CourseDTO course, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateCourse(CourseUpdateDTO course, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteCourse(Guid id, CancellationToken cancellationToken = default);
}