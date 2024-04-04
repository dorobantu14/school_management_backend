using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;

namespace MobyLabWebProgramming.Infrastructure.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options, DbSet<TeacherDTO?> teachers)
        : base(options)
    {
        Teachers = teachers;
    }

    public DbSet<TeacherDTO?> Teachers { get; set; }
}