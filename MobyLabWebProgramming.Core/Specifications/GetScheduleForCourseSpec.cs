using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class GetScheduleForCourseSpec : BaseSpec<GetScheduleForCourseSpec, Schedule, GetSchedulesForCourseDTO>
{
    protected override Expression<Func<Schedule, GetSchedulesForCourseDTO>> Spec { get; } = s => new GetSchedulesForCourseDTO
    {
        DayOfWeek = s.DayOfWeek,
        StartHour = s.StartHour,
        EndHour = s.EndHour,
        ClassroomName = s.Classroom.Name,
        TeacherName = s.Teacher.Name
    };

    public GetScheduleForCourseSpec(Guid courseId)
    {
        Query.Where(s => s.CourseId == courseId);
    }
}