using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuxiliaryMeans",
                columns: table => new
                {
                    IdMean = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Ammount = table.Column<int>(type: "int", nullable: false),
                    NameMean = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    State = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuxiliaryMeans", x => x.IdMean);
                });

            migrationBuilder.CreateTable(
                name: "ClassRoom",
                columns: table => new
                {
                    IdClassR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAviable = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoom", x => x.IdClassR);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    IdC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.IdC);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    IdM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.IdM);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    IdProf = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameProf = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    LastNameProf = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Contract = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    IsDean = table.Column<bool>(type: "bit", nullable: false),
                    LaboralExperience = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.IdProf);
                });

            migrationBuilder.CreateTable(
                name: "Restrction",
                columns: table => new
                {
                    IdRes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRes = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrction", x => x.IdRes);
                });

            migrationBuilder.CreateTable(
                name: "Secretary",
                columns: table => new
                {
                    IdS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameS = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    LastNameS = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    SalaryS = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretary", x => x.IdS);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    IdStud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameStud = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    EActivity = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.IdStud);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    IdSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSub = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    StudyProgram = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CourseLoad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.IdSub);
                });

            migrationBuilder.CreateTable(
                name: "TechnologicalMeans",
                columns: table => new
                {
                    IdMean = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ammount = table.Column<int>(type: "int", nullable: false),
                    NameMean = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    State = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologicalMeans", x => x.IdMean);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorSubject",
                columns: table => new
                {
                    IdProf = table.Column<int>(type: "int", nullable: false),
                    IdSub = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSubject", x => new { x.IdProf, x.IdSub });
                    table.ForeignKey(
                        name: "FK_ProfessorSubject_Professor_IdSub",
                        column: x => x.IdSub,
                        principalTable: "Professor",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorSubject_Subject_IdProf",
                        column: x => x.IdProf,
                        principalTable: "Subject",
                        principalColumn: "IdSub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentSubject",
                columns: table => new
                {
                    IdStud = table.Column<int>(type: "int", nullable: false),
                    IdSub = table.Column<int>(type: "int", nullable: false),
                    NJAbsents = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentSubject", x => new { x.IdStud, x.IdSub });
                    table.ForeignKey(
                        name: "FK_studentSubject_Student_IdSub",
                        column: x => x.IdSub,
                        principalTable: "Student",
                        principalColumn: "IdStud",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_studentSubject_Subject_IdStud",
                        column: x => x.IdStud,
                        principalTable: "Subject",
                        principalColumn: "IdSub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubject_IdSub",
                table: "ProfessorSubject",
                column: "IdSub");

            migrationBuilder.CreateIndex(
                name: "IX_studentSubject_IdSub",
                table: "studentSubject",
                column: "IdSub");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuxiliaryMeans");

            migrationBuilder.DropTable(
                name: "ClassRoom");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "ProfessorSubject");

            migrationBuilder.DropTable(
                name: "Restrction");

            migrationBuilder.DropTable(
                name: "Secretary");

            migrationBuilder.DropTable(
                name: "studentSubject");

            migrationBuilder.DropTable(
                name: "TechnologicalMeans");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}
