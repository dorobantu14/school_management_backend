using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class GetTeacherScheduleByEmailSpec : BaseSpec<GetTeacherScheduleByEmailSpec, Schedule, GetTeacherScheduleByEmailDTO>
{
    protected override Expression<Func<Schedule, GetTeacherScheduleByEmailDTO>> Spec { get; } = e => new()
    {
        DayOfWeek = e.DayOfWeek,
        StartHour = e.StartHour,
        EndHour = e.EndHour,
        Course = new CourseDTO
        {
            Name = e.Course.Name,
        },
        Classroom = new ClassroomDTO
        {
            Name = e.Classroom.Name,
        },
    };
    
    public GetTeacherScheduleByEmailSpec(String email)
    {
        Query.Include(s => s.Course).Include(s => s.Classroom).Where(s => s.Teacher.Email == email);
    }
}