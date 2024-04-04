using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class GetTeachersByCourseSpec : BaseSpec<GetTeachersByCourseSpec, Teacher, GetTeachersByCourseDTO>
{
    protected override Expression<Func<Teacher, GetTeachersByCourseDTO>> Spec { get; } = e => new()
    {
        Name = e.Name,
        
    };
    
    public GetTeachersByCourseSpec(Guid courseId)
    {
        Query.Where(e => e.CourseId == courseId);
    }
}