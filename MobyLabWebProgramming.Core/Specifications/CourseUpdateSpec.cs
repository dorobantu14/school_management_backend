using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CourseUpdateSpec : BaseSpec<CourseUpdateSpec, Course>
{
    public CourseUpdateSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
}