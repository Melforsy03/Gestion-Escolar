using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorSubject",
                table: "ProfessorSubject");

            migrationBuilder.AddColumn<int>(
                name: "IdProfSub",
                table: "ProfessorSubject",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorSubject",
                table: "ProfessorSubject",
                column: "IdProfSub");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubject_IdProf",
                table: "ProfessorSubject",
                column: "IdProf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorSubject",
                table: "ProfessorSubject");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorSubject_IdProf",
                table: "ProfessorSubject");

            migrationBuilder.DropColumn(
                name: "IdProfSub",
                table: "ProfessorSubject");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorSubject",
                table: "ProfessorSubject",
                columns: new[] { "IdProf", "IdSub" });
        }
    }
}
