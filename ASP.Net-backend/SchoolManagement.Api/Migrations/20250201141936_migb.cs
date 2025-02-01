using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class migb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorClassRoom_ClassRooms_IdClassR",
                table: "ProfessorClassRoom");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorClassRoom_Professors_IdProf",
                table: "ProfessorClassRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorClassRoom",
                table: "ProfessorClassRoom");

            migrationBuilder.RenameTable(
                name: "ProfessorClassRoom",
                newName: "ProfessorClassRooms");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorClassRoom_IdProf",
                table: "ProfessorClassRooms",
                newName: "IX_ProfessorClassRooms_IdProf");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorClassRoom_IdClassR",
                table: "ProfessorClassRooms",
                newName: "IX_ProfessorClassRooms_IdClassR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorClassRooms",
                table: "ProfessorClassRooms",
                column: "IdProfClass");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorClassRooms_ClassRooms_IdClassR",
                table: "ProfessorClassRooms",
                column: "IdClassR",
                principalTable: "ClassRooms",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorClassRooms_Professors_IdProf",
                table: "ProfessorClassRooms",
                column: "IdProf",
                principalTable: "Professors",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorClassRooms_ClassRooms_IdClassR",
                table: "ProfessorClassRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorClassRooms_Professors_IdProf",
                table: "ProfessorClassRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorClassRooms",
                table: "ProfessorClassRooms");

            migrationBuilder.RenameTable(
                name: "ProfessorClassRooms",
                newName: "ProfessorClassRoom");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorClassRooms_IdProf",
                table: "ProfessorClassRoom",
                newName: "IX_ProfessorClassRoom_IdProf");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorClassRooms_IdClassR",
                table: "ProfessorClassRoom",
                newName: "IX_ProfessorClassRoom_IdClassR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorClassRoom",
                table: "ProfessorClassRoom",
                column: "IdProfClass");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorClassRoom_ClassRooms_IdClassR",
                table: "ProfessorClassRoom",
                column: "IdClassR",
                principalTable: "ClassRooms",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorClassRoom_Professors_IdProf",
                table: "ProfessorClassRoom",
                column: "IdProf",
                principalTable: "Professors",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
