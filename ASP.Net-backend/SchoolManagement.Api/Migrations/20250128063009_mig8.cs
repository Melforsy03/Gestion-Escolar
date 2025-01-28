using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjects_Professors_IdSub",
                table: "ProfessorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjects_Subjects_IdProf",
                table: "ProfessorSubjects");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjects_Professors_IdProf",
                table: "ProfessorSubjects",
                column: "IdProf",
                principalTable: "Professors",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjects_Subjects_IdSub",
                table: "ProfessorSubjects",
                column: "IdSub",
                principalTable: "Subjects",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjects_Professors_IdProf",
                table: "ProfessorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjects_Subjects_IdSub",
                table: "ProfessorSubjects");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjects_Professors_IdSub",
                table: "ProfessorSubjects",
                column: "IdSub",
                principalTable: "Professors",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjects_Subjects_IdProf",
                table: "ProfessorSubjects",
                column: "IdProf",
                principalTable: "Subjects",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
