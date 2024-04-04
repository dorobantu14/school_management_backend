using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CourseSpec : BaseSpec<CourseSpec, Course, CourseDTO>
{
    protected override Expression<Func<Course, CourseDTO>> Spec { get; } = e => new()
    {
        Name = e.Name,
    };
    
    public CourseSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
}