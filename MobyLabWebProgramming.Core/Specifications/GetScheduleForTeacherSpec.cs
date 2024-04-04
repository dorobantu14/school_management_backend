using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class GetScheduleForTeacherSpec : BaseSpec<GetScheduleForTeacherSpec, Schedule, GetScheduleForTeacherDTO>
{
    protected override Expression<Func<Schedule, GetScheduleForTeacherDTO>> Spec { get; } = e => new()
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
        }
    };
    
    public GetScheduleForTeacherSpec(Guid teacherId)
    {
        Query.Include(s => s.Course).Where(s => s.TeacherId == teacherId);
    }
}