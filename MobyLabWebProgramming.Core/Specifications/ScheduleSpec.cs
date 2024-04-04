using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ScheduleSpec : BaseSpec<ScheduleSpec, Schedule, ScheduleDTO>
{
    protected override Expression<Func<Schedule, ScheduleDTO>> Spec { get; } = e => new()
    {
        StartHour = e.StartHour,
        EndHour = e.EndHour,
        DayOfWeek = e.DayOfWeek,
        ClassroomId = e.ClassroomId,
        CourseId = e.CourseId
    };
    
    public ScheduleSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
}