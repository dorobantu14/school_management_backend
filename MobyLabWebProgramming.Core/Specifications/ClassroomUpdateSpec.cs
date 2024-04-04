using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ClassroomUpdateSpec : BaseSpec<ClassroomUpdateSpec, Classroom>
{
public ClassroomUpdateSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
}