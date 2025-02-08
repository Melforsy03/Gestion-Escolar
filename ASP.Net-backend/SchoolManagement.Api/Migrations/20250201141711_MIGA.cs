using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class MIGA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassRoomProfessor");

            migrationBuilder.CreateTable(
                name: "ProfessorClassRoom",
                columns: table => new
                {
                    IdProfClass = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProf = table.Column<int>(type: "int", nullable: false),
                    IdClassR = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorClassRoom", x => x.IdProfClass);
                    table.ForeignKey(
                        name: "FK_ProfessorClassRoom_ClassRooms_IdClassR",
                        column: x => x.IdClassR,
                        principalTable: "ClassRooms",
                        principalColumn: "IdClassR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorClassRoom_Professors_IdProf",
                        column: x => x.IdProf,
                        principalTable: "Professors",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorClassRoom_IdClassR",
                table: "ProfessorClassRoom",
                column: "IdClassR");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorClassRoom_IdProf",
                table: "ProfessorClassRoom",
                column: "IdProf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessorClassRoom");

            migrationBuilder.CreateTable(
                name: "ClassRoomProfessor",
                columns: table => new
                {
                    ClassRoomsIdClassR = table.Column<int>(type: "int", nullable: false),
                    ProfessorsIdProf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoomProfessor", x => new { x.ClassRoomsIdClassR, x.ProfessorsIdProf });
                    table.ForeignKey(
                        name: "FK_ClassRoomProfessor_ClassRooms_ClassRoomsIdClassR",
                        column: x => x.ClassRoomsIdClassR,
                        principalTable: "ClassRooms",
                        principalColumn: "IdClassR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassRoomProfessor_Professors_ProfessorsIdProf",
                        column: x => x.ProfessorsIdProf,
                        principalTable: "Professors",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomProfessor_ProfessorsIdProf",
                table: "ClassRoomProfessor",
                column: "ProfessorsIdProf");
        }
    }
}
