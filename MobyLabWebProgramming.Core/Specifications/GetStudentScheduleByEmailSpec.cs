using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class GetStudentClassByEmailSpec : BaseSpec<GetStudentClassByEmailSpec, Classroom, GetStudentClassByEmailDTO>
{
    protected override Expression<Func<Classroom, GetStudentClassByEmailDTO>> Spec { get; } = e => new()
    {
        ClassroomId = e.Id,
    };
    
    public GetStudentClassByEmailSpec(String email)
    {
        Query.Where(c => c.Students.Any(s => s.Email == email));
    }
}