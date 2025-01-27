using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomProfessor_ClassRooms_IdProf",
                table: "ClassRoomProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomProfessor_Professors_IdClassRoom",
                table: "ClassRoomProfessor");

            migrationBuilder.DropTable(
                name: "AuxMeansProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRoomProfessor",
                table: "ClassRoomProfessor");

            migrationBuilder.DropIndex(
                name: "IX_ClassRoomProfessor_IdClassRoom",
                table: "ClassRoomProfessor");

            migrationBuilder.DropColumn(
                name: "IdClassRoomProf",
                table: "ClassRoomProfessor");

            migrationBuilder.RenameColumn(
                name: "IdProf",
                table: "ClassRoomProfessor",
                newName: "ProfessorsIdProf");

            migrationBuilder.RenameColumn(
                name: "IdClassRoom",
                table: "ClassRoomProfessor",
                newName: "ClassRoomsIdClassR");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomProfessor_IdProf",
                table: "ClassRoomProfessor",
                newName: "IX_ClassRoomProfessor_ProfessorsIdProf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRoomProfessor",
                table: "ClassRoomProfessor",
                columns: new[] { "ClassRoomsIdClassR", "ProfessorsIdProf" });

            migrationBuilder.CreateTable(
                name: "AuxiliaryMeansProfessor",
                columns: table => new
                {
                    AuxiliaryMeansIdMean = table.Column<int>(type: "int", nullable: false),
                    ProfessorsIdProf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuxiliaryMeansProfessor", x => new { x.AuxiliaryMeansIdMean, x.ProfessorsIdProf });
                    table.ForeignKey(
                        name: "FK_AuxiliaryMeansProfessor_AuxiliaryMeans_AuxiliaryMeansIdMean",
                        column: x => x.AuxiliaryMeansIdMean,
                        principalTable: "AuxiliaryMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuxiliaryMeansProfessor_Professors_ProfessorsIdProf",
                        column: x => x.ProfessorsIdProf,
                        principalTable: "Professors",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuxiliaryMeansProfessor_ProfessorsIdProf",
                table: "AuxiliaryMeansProfessor",
                column: "ProfessorsIdProf");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomProfessor_ClassRooms_ClassRoomsIdClassR",
                table: "ClassRoomProfessor",
                column: "ClassRoomsIdClassR",
                principalTable: "ClassRooms",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomProfessor_Professors_ProfessorsIdProf",
                table: "ClassRoomProfessor",
                column: "ProfessorsIdProf",
                principalTable: "Professors",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomProfessor_ClassRooms_ClassRoomsIdClassR",
                table: "ClassRoomProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomProfessor_Professors_ProfessorsIdProf",
                table: "ClassRoomProfessor");

            migrationBuilder.DropTable(
                name: "AuxiliaryMeansProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRoomProfessor",
                table: "ClassRoomProfessor");

            migrationBuilder.RenameColumn(
                name: "ProfessorsIdProf",
                table: "ClassRoomProfessor",
                newName: "IdProf");

            migrationBuilder.RenameColumn(
                name: "ClassRoomsIdClassR",
                table: "ClassRoomProfessor",
                newName: "IdClassRoom");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomProfessor_ProfessorsIdProf",
                table: "ClassRoomProfessor",
                newName: "IX_ClassRoomProfessor_IdProf");

            migrationBuilder.AddColumn<int>(
                name: "IdClassRoomProf",
                table: "ClassRoomProfessor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRoomProfessor",
                table: "ClassRoomProfessor",
                column: "IdClassRoomProf");

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

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomProfessor_IdClassRoom",
                table: "ClassRoomProfessor",
                column: "IdClassRoom");

            migrationBuilder.CreateIndex(
                name: "IX_AuxMeansProfessor_IdAuxMean",
                table: "AuxMeansProfessor",
                column: "IdAuxMean");

            migrationBuilder.CreateIndex(
                name: "IX_AuxMeansProfessor_IdProf",
                table: "AuxMeansProfessor",
                column: "IdProf");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomProfessor_ClassRooms_IdProf",
                table: "ClassRoomProfessor",
                column: "IdProf",
                principalTable: "ClassRooms",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomProfessor_Professors_IdClassRoom",
                table: "ClassRoomProfessor",
                column: "IdClassRoom",
                principalTable: "Professors",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
