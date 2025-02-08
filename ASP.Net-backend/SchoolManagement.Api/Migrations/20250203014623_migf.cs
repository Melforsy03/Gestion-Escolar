using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class migf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeanNotifications",
                columns: table => new
                {
                    MeanNotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeanID = table.Column<int>(type: "int", nullable: false),
                    BeenSended = table.Column<bool>(type: "bit", nullable: false),
                    MeanType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeanNotifications", x => x.MeanNotId);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorNotifications",
                columns: table => new
                {
                    ProfNotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProf = table.Column<int>(type: "int", nullable: false),
                    ProfName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeenSended = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorNotifications", x => x.ProfNotId);
                    table.ForeignKey(
                        name: "FK_ProfessorNotifications_Professors_IdProf",
                        column: x => x.IdProf,
                        principalTable: "Professors",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorNotifications_IdProf",
                table: "ProfessorNotifications",
                column: "IdProf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeanNotifications");

            migrationBuilder.DropTable(
                name: "ProfessorNotifications");
        }
    }
}
