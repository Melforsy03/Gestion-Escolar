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
            migrationBuilder.CreateTable(
                name: "AuxMeansProfessor",
                columns: table => new
                {
                    IdAuxMeanProf = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAuxMean = table.Column<int>(type: "int", nullable: false),
                    IdProf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuxMeansProfessor", x => x.IdAuxMeanProf);
                    table.ForeignKey(
                        name: "FK_AuxMeansProfessor_AuxiliaryMeans_IdProf",
                        column: x => x.IdProf,
                        principalTable: "AuxiliaryMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuxMeansProfessor_Professors_IdAuxMean",
                        column: x => x.IdAuxMean,
                        principalTable: "Professors",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassRoomProfessor",
                columns: table => new
                {
                    IdClassRoomProf = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClassRoom = table.Column<int>(type: "int", nullable: false),
                    IdProf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoomProfessor", x => x.IdClassRoomProf);
                    table.ForeignKey(
                        name: "FK_ClassRoomProfessor_ClassRooms_IdProf",
                        column: x => x.IdProf,
                        principalTable: "ClassRooms",
                        principalColumn: "IdClassR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassRoomProfessor_Professors_IdClassRoom",
                        column: x => x.IdClassRoom,
                        principalTable: "Professors",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuxMeansProfessor_IdAuxMean",
                table: "AuxMeansProfessor",
                column: "IdAuxMean");

            migrationBuilder.CreateIndex(
                name: "IX_AuxMeansProfessor_IdProf",
                table: "AuxMeansProfessor",
                column: "IdProf");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomProfessor_IdClassRoom",
                table: "ClassRoomProfessor",
                column: "IdClassRoom");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomProfessor_IdProf",
                table: "ClassRoomProfessor",
                column: "IdProf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuxMeansProfessor");

            migrationBuilder.DropTable(
                name: "ClassRoomProfessor");
        }
    }
}
