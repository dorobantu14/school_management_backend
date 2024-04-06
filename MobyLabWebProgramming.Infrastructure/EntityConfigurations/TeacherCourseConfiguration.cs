using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class TeacherCourseConfiguration : IEntityTypeConfiguration<TeacherCourses>
{
    public void Configure(EntityTypeBuilder<TeacherCourses> modelBuilder)
    {
        modelBuilder
            .HasKey(tc => new { tc.TeacherID, tc.CourseID });

        modelBuilder
            .HasOne(tc => tc.Teacher)
            .WithMany(t => t.TeacherCourses)
            .HasForeignKey(tc => tc.TeacherID);

            modelBuilder
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TeacherCourses)
            .HasForeignKey(tc => tc.CourseID);
            
            modelBuilder.Property(t => t.CreatedAt)
                .IsRequired();

            modelBuilder.Property(t => t.UpdatedAt)
                .IsRequired();
    }
}
