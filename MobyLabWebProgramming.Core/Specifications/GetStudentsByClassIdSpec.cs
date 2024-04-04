using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class GetStudentsByClassIdSpec : BaseSpec<GetStudentsByClassIdSpec, Student, GetStudentsByClassDTO>
{
    protected override Expression<Func<Student, GetStudentsByClassDTO>> Spec { get; } = e => new()
    {
        Name = e.Name,
    };
    
    public GetStudentsByClassIdSpec(Guid classroomId)
    {
        Query.Where(e => e.ClassroomId == classroomId);
    }
}