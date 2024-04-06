using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options, DbSet<Teacher?> teachers, DbSet<Course> courses, DbSet<TeacherCourses> teacherCourses)
        : base(options)
    {
        Teachers = teachers;
        Courses = courses;
        TeacherCourses = teacherCourses;
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<TeacherCourses> TeacherCourses { get; set; }
}