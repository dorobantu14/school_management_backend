using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TeacherSpec : BaseSpec<TeacherSpec, Teacher, TeacherDTO>
{
    protected override Expression<Func<Teacher, TeacherDTO>> Spec => e => new()
    {
        Name = e.Name,
        Email = e.Email,
    };
    public TeacherSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
}