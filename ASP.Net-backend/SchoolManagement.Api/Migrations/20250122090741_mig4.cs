using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ammount",
                table: "TechnologicalMeans");

            migrationBuilder.DropColumn(
                name: "Ammount",
                table: "AuxiliaryMeans");

            migrationBuilder.DropColumn(
                name: "AviableAmmount",
                table: "AuxiliaryMeans");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "TechnologicalMeans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isAviable",
                table: "TechnologicalMeans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TechnologicalMeans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "AuxiliaryMeans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isAviable",
                table: "AuxiliaryMeans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "AuxiliaryMeans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "TechnologicalMeans");

            migrationBuilder.DropColumn(
                name: "isAviable",
                table: "TechnologicalMeans");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TechnologicalMeans");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "AuxiliaryMeans");

            migrationBuilder.DropColumn(
                name: "isAviable",
                table: "AuxiliaryMeans");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AuxiliaryMeans");

            migrationBuilder.AddColumn<int>(
                name: "Ammount",
                table: "TechnologicalMeans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ammount",
                table: "AuxiliaryMeans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AviableAmmount",
                table: "AuxiliaryMeans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
