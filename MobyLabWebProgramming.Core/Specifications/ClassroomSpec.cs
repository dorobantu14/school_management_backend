using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ClassroomSpec : BaseSpec<ClassroomSpec, Classroom, ClassroomDTO>
{
    protected override Expression<Func<Classroom, ClassroomDTO>> Spec { get; } = e => new()
    {
        Name = e.Name,
    };
            
    
    public ClassroomSpec(Guid id)
    {
       Query.Where(e => e.Id == id);
    }
}