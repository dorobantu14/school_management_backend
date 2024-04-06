using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class TeacherCoursemigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherCourses",
                table: "TeacherCourses");

            migrationBuilder.DropIndex(
                name: "IX_TeacherCourses_TeacherID",
                table: "TeacherCourses");

            migrationBuilder.DropColumn(
                name: "CourseIds",
                table: "TeacherCourses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherCourses",
                table: "TeacherCourses",
                columns: new[] { "TeacherID", "CourseID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherCourses",
                table: "TeacherCourses");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "CourseIds",
                table: "TeacherCourses",
                type: "uuid[]",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherCourses",
                table: "TeacherCourses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_TeacherID",
                table: "TeacherCourses",
                column: "TeacherID");
        }
    }
}
