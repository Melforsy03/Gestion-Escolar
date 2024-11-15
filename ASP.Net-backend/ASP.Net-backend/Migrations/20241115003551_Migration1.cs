using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.Net_backend.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profesor",
                columns: table => new
                {
                    IdP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreP = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    TipoContrato = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Salario = table.Column<int>(type: "int", nullable: false),
                    EsDecano = table.Column<bool>(type: "bit", nullable: false),
                    FueBorrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesor", x => x.IdP);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profesor");
        }
    }
}
