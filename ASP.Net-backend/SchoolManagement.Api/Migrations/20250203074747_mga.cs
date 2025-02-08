using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class mga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMaintenance",
                table: "TechnologicalMeans");

            migrationBuilder.DropColumn(
                name: "IdMaintenance",
                table: "AuxiliaryMeans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMaintenance",
                table: "TechnologicalMeans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdMaintenance",
                table: "AuxiliaryMeans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
