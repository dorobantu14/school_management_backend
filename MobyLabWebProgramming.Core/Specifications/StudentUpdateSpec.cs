using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class StudentUpdateSpec : BaseSpec<StudentUpdateSpec, Student>
{
    public StudentUpdateSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
}