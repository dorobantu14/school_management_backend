using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ScheduleConfigurations : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.Property(s => s.Id)
            .IsRequired();
        builder.HasKey(s => s.Id);

        builder.Property(s => s.DayOfWeek)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(s => s.StartHour)
            .IsRequired();

        builder.Property(s => s.EndHour)
            .IsRequired();

        builder.Property(s => s.CourseId)
            .IsRequired();

        // relatie one-to-many
        builder.
            HasOne<Course>(s => s.Course)
            .WithMany(c => c.Schedules)
            .HasForeignKey(s => s.CourseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        // relatie one-to-many
        builder.HasOne<Classroom>(s => s.Classroom)
            .WithMany(c => c.Schedules)
            .HasForeignKey(s => s.ClassroomId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.Property(s => s.UpdatedAt)
            .IsRequired();
    }
}