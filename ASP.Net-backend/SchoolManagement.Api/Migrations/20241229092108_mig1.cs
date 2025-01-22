using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
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
                    State = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdMaintenance = table.Column<int>(type: "int", nullable: false)
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.IdC);
                });

            migrationBuilder.CreateTable(
                name: "Restriction",
                columns: table => new
                {
                    IdRes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRes = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restriction", x => x.IdRes);
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
                name: "TechnologicalMeans",
                columns: table => new
                {
                    IdMean = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ammount = table.Column<int>(type: "int", nullable: false),
                    NameMean = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    State = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdMaintenance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologicalMeans", x => x.IdMean);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    IdSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSub = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    StudyProgram = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CourseLoad = table.Column<int>(type: "int", nullable: false),
                    IdClassRoom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.IdSub);
                    table.ForeignKey(
                        name: "FK_Subject_ClassRoom_IdClassRoom",
                        column: x => x.IdClassRoom,
                        principalTable: "ClassRoom",
                        principalColumn: "IdClassR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    IdStud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameStud = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    EActivity = table.Column<bool>(type: "bit", nullable: false),
                    IdC = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.IdStud);
                    table.ForeignKey(
                        name: "FK_Student_Course_IdC",
                        column: x => x.IdC,
                        principalTable: "Course",
                        principalColumn: "IdC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassRoomRestriction",
                columns: table => new
                {
                    IdClassRoomRest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClassRoom = table.Column<int>(type: "int", nullable: false),
                    IdRest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoomRestriction", x => x.IdClassRoomRest);
                    table.ForeignKey(
                        name: "FK_ClassRoomRestriction_ClassRoom_IdRest",
                        column: x => x.IdRest,
                        principalTable: "ClassRoom",
                        principalColumn: "IdClassR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassRoomRestriction_Restriction_IdClassRoom",
                        column: x => x.IdClassRoom,
                        principalTable: "Restriction",
                        principalColumn: "IdRes",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassRoomTechMean",
                columns: table => new
                {
                    IdClassRoomTech = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClassRoom = table.Column<int>(type: "int", nullable: false),
                    IdTechMean = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoomTechMean", x => x.IdClassRoomTech);
                    table.ForeignKey(
                        name: "FK_ClassRoomTechMean_ClassRoom_IdTechMean",
                        column: x => x.IdTechMean,
                        principalTable: "ClassRoom",
                        principalColumn: "IdClassR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassRoomTechMean_TechnologicalMeans_IdClassRoom",
                        column: x => x.IdClassRoom,
                        principalTable: "TechnologicalMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    IdM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    typeOfMean = table.Column<int>(type: "int", nullable: false),
                    IdAuxMean = table.Column<int>(type: "int", nullable: false),
                    IdTechMean = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.IdM);
                    table.ForeignKey(
                        name: "FK_Maintenance_AuxiliaryMeans_IdAuxMean",
                        column: x => x.IdAuxMean,
                        principalTable: "AuxiliaryMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintenance_TechnologicalMeans_IdTechMean",
                        column: x => x.IdTechMean,
                        principalTable: "TechnologicalMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectAuxMean",
                columns: table => new
                {
                    IdSubAuxMean = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSub = table.Column<int>(type: "int", nullable: false),
                    IdAuxMean = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAuxMean", x => x.IdSubAuxMean);
                    table.ForeignKey(
                        name: "FK_SubjectAuxMean_AuxiliaryMeans_IdSub",
                        column: x => x.IdSub,
                        principalTable: "AuxiliaryMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectAuxMean_Subject_IdAuxMean",
                        column: x => x.IdAuxMean,
                        principalTable: "Subject",
                        principalColumn: "IdSub",
                        onDelete: ReferentialAction.Cascade);
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
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StudentIdStud = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.IdProf);
                    table.ForeignKey(
                        name: "FK_Professor_Student_StudentIdStud",
                        column: x => x.StudentIdStud,
                        principalTable: "Student",
                        principalColumn: "IdStud");
                });

            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    IdStudSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdStud = table.Column<int>(type: "int", nullable: false),
                    IdSub = table.Column<int>(type: "int", nullable: false),
                    NJAbsents = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => x.IdStudSub);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Student_IdSub",
                        column: x => x.IdSub,
                        principalTable: "Student",
                        principalColumn: "IdStud",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Subject_IdStud",
                        column: x => x.IdStud,
                        principalTable: "Subject",
                        principalColumn: "IdSub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorSubject",
                columns: table => new
                {
                    IdProfSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProf = table.Column<int>(type: "int", nullable: false),
                    IdSub = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSubject", x => x.IdProfSub);
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
                name: "ProfStudSubCourse",
                columns: table => new
                {
                    IdProfStudSubCourse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProf = table.Column<int>(type: "int", nullable: false),
                    IdStud = table.Column<int>(type: "int", nullable: false),
                    IdSub = table.Column<int>(type: "int", nullable: false),
                    IdCourse = table.Column<int>(type: "int", nullable: false),
                    Evaluation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfStudSubCourse", x => x.IdProfStudSubCourse);
                    table.ForeignKey(
                        name: "FK_ProfStudSubCourse_Course_IdCourse",
                        column: x => x.IdCourse,
                        principalTable: "Course",
                        principalColumn: "IdC");
                    table.ForeignKey(
                        name: "FK_ProfStudSubCourse_Professor_IdProf",
                        column: x => x.IdProf,
                        principalTable: "Professor",
                        principalColumn: "IdProf");
                    table.ForeignKey(
                        name: "FK_ProfStudSubCourse_Student_IdStud",
                        column: x => x.IdStud,
                        principalTable: "Student",
                        principalColumn: "IdStud");
                    table.ForeignKey(
                        name: "FK_ProfStudSubCourse_Subject_IdSub",
                        column: x => x.IdSub,
                        principalTable: "Subject",
                        principalColumn: "IdSub");
                });

            migrationBuilder.CreateTable(
                name: "ProfessorStudentSubject",
                columns: table => new
                {
                    IdProfStudSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProf = table.Column<int>(type: "int", nullable: false),
                    IdStudSub = table.Column<int>(type: "int", nullable: false),
                    StudentGrades = table.Column<float>(type: "real", nullable: false, defaultValue: 0f)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorStudentSubject", x => x.IdProfStudSub);
                    table.ForeignKey(
                        name: "FK_ProfessorStudentSubject_Professor_IdProf",
                        column: x => x.IdProf,
                        principalTable: "Professor",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorStudentSubject_StudentSubject_IdStudSub",
                        column: x => x.IdStudSub,
                        principalTable: "StudentSubject",
                        principalColumn: "IdStudSub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecretaryProfessorStudentSubject",
                columns: table => new
                {
                    IdSecProfStudSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSec = table.Column<int>(type: "int", nullable: false),
                    IdProfStudSub = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretaryProfessorStudentSubject", x => x.IdSecProfStudSub);
                    table.ForeignKey(
                        name: "FK_SecretaryProfessorStudentSubject_ProfessorStudentSubject_IdSec",
                        column: x => x.IdSec,
                        principalTable: "ProfessorStudentSubject",
                        principalColumn: "IdProfStudSub",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecretaryProfessorStudentSubject_Secretary_IdProfStudSub",
                        column: x => x.IdProfStudSub,
                        principalTable: "Secretary",
                        principalColumn: "IdS",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomRestriction_IdClassRoom",
                table: "ClassRoomRestriction",
                column: "IdClassRoom");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomRestriction_IdRest",
                table: "ClassRoomRestriction",
                column: "IdRest");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomTechMean_IdClassRoom",
                table: "ClassRoomTechMean",
                column: "IdClassRoom");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomTechMean_IdTechMean",
                table: "ClassRoomTechMean",
                column: "IdTechMean");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_IdAuxMean",
                table: "Maintenance",
                column: "IdAuxMean");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_IdTechMean",
                table: "Maintenance",
                column: "IdTechMean");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_StudentIdStud",
                table: "Professor",
                column: "StudentIdStud");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorStudentSubject_IdProf",
                table: "ProfessorStudentSubject",
                column: "IdProf");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorStudentSubject_IdStudSub",
                table: "ProfessorStudentSubject",
                column: "IdStudSub");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubject_IdProf",
                table: "ProfessorSubject",
                column: "IdProf");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubject_IdSub",
                table: "ProfessorSubject",
                column: "IdSub");

            migrationBuilder.CreateIndex(
                name: "IX_ProfStudSubCourse_IdCourse",
                table: "ProfStudSubCourse",
                column: "IdCourse");

            migrationBuilder.CreateIndex(
                name: "IX_ProfStudSubCourse_IdProf",
                table: "ProfStudSubCourse",
                column: "IdProf");

            migrationBuilder.CreateIndex(
                name: "IX_ProfStudSubCourse_IdStud",
                table: "ProfStudSubCourse",
                column: "IdStud");

            migrationBuilder.CreateIndex(
                name: "IX_ProfStudSubCourse_IdSub",
                table: "ProfStudSubCourse",
                column: "IdSub");

            migrationBuilder.CreateIndex(
                name: "IX_SecretaryProfessorStudentSubject_IdProfStudSub",
                table: "SecretaryProfessorStudentSubject",
                column: "IdProfStudSub");

            migrationBuilder.CreateIndex(
                name: "IX_SecretaryProfessorStudentSubject_IdSec",
                table: "SecretaryProfessorStudentSubject",
                column: "IdSec");

            migrationBuilder.CreateIndex(
                name: "IX_Student_IdC",
                table: "Student",
                column: "IdC");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_IdStud",
                table: "StudentSubject",
                column: "IdStud");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_IdSub",
                table: "StudentSubject",
                column: "IdSub");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_IdClassRoom",
                table: "Subject",
                column: "IdClassRoom");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAuxMean_IdAuxMean",
                table: "SubjectAuxMean",
                column: "IdAuxMean");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAuxMean_IdSub",
                table: "SubjectAuxMean",
                column: "IdSub");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassRoomRestriction");

            migrationBuilder.DropTable(
                name: "ClassRoomTechMean");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "ProfessorSubject");

            migrationBuilder.DropTable(
                name: "ProfStudSubCourse");

            migrationBuilder.DropTable(
                name: "SecretaryProfessorStudentSubject");

            migrationBuilder.DropTable(
                name: "SubjectAuxMean");

            migrationBuilder.DropTable(
                name: "Restriction");

            migrationBuilder.DropTable(
                name: "TechnologicalMeans");

            migrationBuilder.DropTable(
                name: "ProfessorStudentSubject");

            migrationBuilder.DropTable(
                name: "Secretary");

            migrationBuilder.DropTable(
                name: "AuxiliaryMeans");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "StudentSubject");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "ClassRoom");
        }
    }
}
