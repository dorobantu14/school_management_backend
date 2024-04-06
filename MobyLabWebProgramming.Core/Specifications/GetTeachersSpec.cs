using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class GetTeachersSpec : BaseSpec<GetTeachersSpec, Teacher, TeacherDTO>
{
    protected override Expression<Func<Teacher, TeacherDTO>> Spec { get; } = e => new()
    {
        Name = e.Name,
        Email = e.Email,
    };
    
    public GetTeachersSpec()
    {
        Query.Select(e => new TeacherDTO
        {
            Name = e.Name,
            Email = e.Email,
        });
    }
    
}