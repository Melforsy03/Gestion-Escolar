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
                name: "Administrator",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    AdminSalary = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuxiliaryMeans",
                columns: table => new
                {
                    IdMean = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    isAviable = table.Column<bool>(type: "bit", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NameMean = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    State = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdMaintenance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuxiliaryMeans", x => x.IdMean);
                });

            migrationBuilder.CreateTable(
                name: "ClassRooms",
                columns: table => new
                {
                    IdClassR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAviable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRooms", x => x.IdClassR);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    IdC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.IdC);
                });

            migrationBuilder.CreateTable(
                name: "Restrictions",
                columns: table => new
                {
                    IdRes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NameRes = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrictions", x => x.IdRes);
                });

            migrationBuilder.CreateTable(
                name: "Secretaries",
                columns: table => new
                {
                    IdS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameS = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastNameS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryS = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretaries", x => x.IdS);
                });

            migrationBuilder.CreateTable(
                name: "TechnologicalMeans",
                columns: table => new
                {
                    IdMean = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isAviable = table.Column<bool>(type: "bit", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NameMean = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    State = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdMaintenance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologicalMeans", x => x.IdMean);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    IdSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSub = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    StudyProgram = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CourseLoad = table.Column<int>(type: "int", nullable: false),
                    IdClassRoom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.IdSub);
                    table.ForeignKey(
                        name: "FK_Subjects_ClassRooms_IdClassRoom",
                        column: x => x.IdClassRoom,
                        principalTable: "ClassRooms",
                        principalColumn: "IdClassR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    IdStud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameStud = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    EActivity = table.Column<bool>(type: "bit", nullable: false),
                    IdC = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.IdStud);
                    table.ForeignKey(
                        name: "FK_Students_Courses_IdC",
                        column: x => x.IdC,
                        principalTable: "Courses",
                        principalColumn: "IdC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassRoomRestrictions",
                columns: table => new
                {
                    IdClassRoomRest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClassRoom = table.Column<int>(type: "int", nullable: false),
                    IdRest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoomRestrictions", x => x.IdClassRoomRest);
                    table.ForeignKey(
                        name: "FK_ClassRoomRestrictions_ClassRooms_IdRest",
                        column: x => x.IdRest,
                        principalTable: "ClassRooms",
                        principalColumn: "IdClassR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassRoomRestrictions_Restrictions_IdClassRoom",
                        column: x => x.IdClassRoom,
                        principalTable: "Restrictions",
                        principalColumn: "IdRes",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassRoomTechMeans",
                columns: table => new
                {
                    IdClassRoomTech = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClassRoom = table.Column<int>(type: "int", nullable: false),
                    IdTechMean = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoomTechMeans", x => x.IdClassRoomTech);
                    table.ForeignKey(
                        name: "FK_ClassRoomTechMeans_ClassRooms_IdTechMean",
                        column: x => x.IdTechMean,
                        principalTable: "ClassRooms",
                        principalColumn: "IdClassR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassRoomTechMeans_TechnologicalMeans_IdClassRoom",
                        column: x => x.IdClassRoom,
                        principalTable: "TechnologicalMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    IdM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    typeOfMean = table.Column<int>(type: "int", nullable: false),
                    IdAuxMean = table.Column<int>(type: "int", nullable: false),
                    IdTechMean = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.IdM);
                    table.ForeignKey(
                        name: "FK_Maintenances_AuxiliaryMeans_IdAuxMean",
                        column: x => x.IdAuxMean,
                        principalTable: "AuxiliaryMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintenances_TechnologicalMeans_IdTechMean",
                        column: x => x.IdTechMean,
                        principalTable: "TechnologicalMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectAuxMeans",
                columns: table => new
                {
                    IdSubAuxMean = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSub = table.Column<int>(type: "int", nullable: false),
                    IdAuxMean = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAuxMeans", x => x.IdSubAuxMean);
                    table.ForeignKey(
                        name: "FK_SubjectAuxMeans_AuxiliaryMeans_IdSub",
                        column: x => x.IdSub,
                        principalTable: "AuxiliaryMeans",
                        principalColumn: "IdMean",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectAuxMeans_Subjects_IdAuxMean",
                        column: x => x.IdAuxMean,
                        principalTable: "Subjects",
                        principalColumn: "IdSub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    IdProf = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    NameProf = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Contract = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    IsDean = table.Column<bool>(type: "bit", nullable: false),
                    LaboralExperience = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StudentIdStud = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.IdProf);
                    table.ForeignKey(
                        name: "FK_Professors_Students_StudentIdStud",
                        column: x => x.StudentIdStud,
                        principalTable: "Students",
                        principalColumn: "IdStud");
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjects",
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
                    table.PrimaryKey("PK_StudentSubjects", x => x.IdStudSub);
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Students_IdSub",
                        column: x => x.IdSub,
                        principalTable: "Students",
                        principalColumn: "IdStud",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Subjects_IdStud",
                        column: x => x.IdStud,
                        principalTable: "Subjects",
                        principalColumn: "IdSub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorSubjects",
                columns: table => new
                {
                    IdProfSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProf = table.Column<int>(type: "int", nullable: false),
                    IdSub = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSubjects", x => x.IdProfSub);
                    table.ForeignKey(
                        name: "FK_ProfessorSubjects_Professors_IdSub",
                        column: x => x.IdSub,
                        principalTable: "Professors",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorSubjects_Subjects_IdProf",
                        column: x => x.IdProf,
                        principalTable: "Subjects",
                        principalColumn: "IdSub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfStudSubCourses",
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
                    table.PrimaryKey("PK_ProfStudSubCourses", x => x.IdProfStudSubCourse);
                    table.ForeignKey(
                        name: "FK_ProfStudSubCourses_Courses_IdCourse",
                        column: x => x.IdCourse,
                        principalTable: "Courses",
                        principalColumn: "IdC");
                    table.ForeignKey(
                        name: "FK_ProfStudSubCourses_Professors_IdProf",
                        column: x => x.IdProf,
                        principalTable: "Professors",
                        principalColumn: "IdProf");
                    table.ForeignKey(
                        name: "FK_ProfStudSubCourses_Students_IdStud",
                        column: x => x.IdStud,
                        principalTable: "Students",
                        principalColumn: "IdStud");
                    table.ForeignKey(
                        name: "FK_ProfStudSubCourses_Subjects_IdSub",
                        column: x => x.IdSub,
                        principalTable: "Subjects",
                        principalColumn: "IdSub");
                });

            migrationBuilder.CreateTable(
                name: "ProfessorStudentSubjects",
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
                    table.PrimaryKey("PK_ProfessorStudentSubjects", x => x.IdProfStudSub);
                    table.ForeignKey(
                        name: "FK_ProfessorStudentSubjects_Professors_IdProf",
                        column: x => x.IdProf,
                        principalTable: "Professors",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorStudentSubjects_StudentSubjects_IdStudSub",
                        column: x => x.IdStudSub,
                        principalTable: "StudentSubjects",
                        principalColumn: "IdStudSub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecretaryProfessorStudentSubjects",
                columns: table => new
                {
                    IdSecProfStudSub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSec = table.Column<int>(type: "int", nullable: false),
                    IdProfStudSub = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretaryProfessorStudentSubjects", x => x.IdSecProfStudSub);
                    table.ForeignKey(
                        name: "FK_SecretaryProfessorStudentSubjects_ProfessorStudentSubjects_IdSec",
                        column: x => x.IdSec,
                        principalTable: "ProfessorStudentSubjects",
                        principalColumn: "IdProfStudSub",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecretaryProfessorStudentSubjects_Secretaries_IdProfStudSub",
                        column: x => x.IdProfStudSub,
                        principalTable: "Secretaries",
                        principalColumn: "IdS",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomRestrictions_IdClassRoom",
                table: "ClassRoomRestrictions",
                column: "IdClassRoom");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomRestrictions_IdRest",
                table: "ClassRoomRestrictions",
                column: "IdRest");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomTechMeans_IdClassRoom",
                table: "ClassRoomTechMeans",
                column: "IdClassRoom");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoomTechMeans_IdTechMean",
                table: "ClassRoomTechMeans",
                column: "IdTechMean");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_IdAuxMean",
                table: "Maintenances",
                column: "IdAuxMean");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_IdTechMean",
                table: "Maintenances",
                column: "IdTechMean");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_StudentIdStud",
                table: "Professors",
                column: "StudentIdStud");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorStudentSubjects_IdProf",
                table: "ProfessorStudentSubjects",
                column: "IdProf");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorStudentSubjects_IdStudSub",
                table: "ProfessorStudentSubjects",
                column: "IdStudSub");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjects_IdProf",
                table: "ProfessorSubjects",
                column: "IdProf");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjects_IdSub",
                table: "ProfessorSubjects",
                column: "IdSub");

            migrationBuilder.CreateIndex(
                name: "IX_ProfStudSubCourses_IdCourse",
                table: "ProfStudSubCourses",
                column: "IdCourse");

            migrationBuilder.CreateIndex(
                name: "IX_ProfStudSubCourses_IdProf",
                table: "ProfStudSubCourses",
                column: "IdProf");

            migrationBuilder.CreateIndex(
                name: "IX_ProfStudSubCourses_IdStud",
                table: "ProfStudSubCourses",
                column: "IdStud");

            migrationBuilder.CreateIndex(
                name: "IX_ProfStudSubCourses_IdSub",
                table: "ProfStudSubCourses",
                column: "IdSub");

            migrationBuilder.CreateIndex(
                name: "IX_SecretaryProfessorStudentSubjects_IdProfStudSub",
                table: "SecretaryProfessorStudentSubjects",
                column: "IdProfStudSub");

            migrationBuilder.CreateIndex(
                name: "IX_SecretaryProfessorStudentSubjects_IdSec",
                table: "SecretaryProfessorStudentSubjects",
                column: "IdSec");

            migrationBuilder.CreateIndex(
                name: "IX_Students_IdC",
                table: "Students",
                column: "IdC");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_IdStud",
                table: "StudentSubjects",
                column: "IdStud");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_IdSub",
                table: "StudentSubjects",
                column: "IdSub");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAuxMeans_IdAuxMean",
                table: "SubjectAuxMeans",
                column: "IdAuxMean");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAuxMeans_IdSub",
                table: "SubjectAuxMeans",
                column: "IdSub");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_IdClassRoom",
                table: "Subjects",
                column: "IdClassRoom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClassRoomRestrictions");

            migrationBuilder.DropTable(
                name: "ClassRoomTechMeans");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "ProfessorSubjects");

            migrationBuilder.DropTable(
                name: "ProfStudSubCourses");

            migrationBuilder.DropTable(
                name: "SecretaryProfessorStudentSubjects");

            migrationBuilder.DropTable(
                name: "SubjectAuxMeans");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropTable(
                name: "TechnologicalMeans");

            migrationBuilder.DropTable(
                name: "ProfessorStudentSubjects");

            migrationBuilder.DropTable(
                name: "Secretaries");

            migrationBuilder.DropTable(
                name: "AuxiliaryMeans");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "StudentSubjects");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "ClassRooms");
        }
    }
}
