using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Professors");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Students",
                newName: "IdUser");

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminLastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    AdminSalary = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.AdminId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Students",
                newName: "userId");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Professors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
