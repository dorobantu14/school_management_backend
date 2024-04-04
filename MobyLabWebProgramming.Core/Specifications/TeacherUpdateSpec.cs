using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TeacherUpdateSpec : BaseSpec<TeacherUpdateSpec, Teacher>
{
    public TeacherUpdateSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
    
}