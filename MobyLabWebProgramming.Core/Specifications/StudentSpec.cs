using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class StudentSpec : BaseSpec<StudentSpec, Student, StudentDTO>
{
    protected override Expression<Func<Student, StudentDTO>> Spec => e => new()
    {
        Name = e.Name,
        Email = e.Email,
        ClassroomId = e.ClassroomId,
    };
    
    public StudentSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
}