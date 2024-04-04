using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class GetSchedulesForClassSpec : BaseSpec<GetSchedulesForClassSpec, Schedule, GetScheduleForClassDTO>
{
    protected override Expression<Func<Schedule, GetScheduleForClassDTO>> Spec { get; } = e => new()
    {
        DayOfWeek = e.DayOfWeek,
        StartHour = e.StartHour,
        EndHour = e.EndHour,
        Course = new CourseDTO
        {
            Name = e.Course.Name,
        },
    };
    
    public GetSchedulesForClassSpec(Guid classroomId)
    {
        Query.Where(e => e.ClassroomId == classroomId);
    }
}