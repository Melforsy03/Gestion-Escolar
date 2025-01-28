using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomRestrictions_ClassRooms_IdRest",
                table: "ClassRoomRestrictions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomRestrictions_Restrictions_IdClassRoom",
                table: "ClassRoomRestrictions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomTechMeans_ClassRooms_IdTechMean",
                table: "ClassRoomTechMeans");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomTechMeans_TechnologicalMeans_IdClassRoom",
                table: "ClassRoomTechMeans");

            migrationBuilder.DropForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_ProfessorStudentSubjects_IdSec",
                table: "SecretaryProfessorStudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_Secretaries_IdProfStudSub",
                table: "SecretaryProfessorStudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_IdSub",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_IdStud",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAuxMeans_AuxiliaryMeans_IdSub",
                table: "SubjectAuxMeans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAuxMeans_Subjects_IdAuxMean",
                table: "SubjectAuxMeans");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomRestrictions_ClassRooms_IdClassRoom",
                table: "ClassRoomRestrictions",
                column: "IdClassRoom",
                principalTable: "ClassRooms",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomRestrictions_Restrictions_IdRest",
                table: "ClassRoomRestrictions",
                column: "IdRest",
                principalTable: "Restrictions",
                principalColumn: "IdRes",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomTechMeans_ClassRooms_IdClassRoom",
                table: "ClassRoomTechMeans",
                column: "IdClassRoom",
                principalTable: "ClassRooms",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomTechMeans_TechnologicalMeans_IdTechMean",
                table: "ClassRoomTechMeans",
                column: "IdTechMean",
                principalTable: "TechnologicalMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_ProfessorStudentSubjects_IdProfStudSub",
                table: "SecretaryProfessorStudentSubjects",
                column: "IdProfStudSub",
                principalTable: "ProfessorStudentSubjects",
                principalColumn: "IdProfStudSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_Secretaries_IdSec",
                table: "SecretaryProfessorStudentSubjects",
                column: "IdSec",
                principalTable: "Secretaries",
                principalColumn: "IdS",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_IdStud",
                table: "StudentSubjects",
                column: "IdStud",
                principalTable: "Students",
                principalColumn: "IdStud",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_IdSub",
                table: "StudentSubjects",
                column: "IdSub",
                principalTable: "Subjects",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAuxMeans_AuxiliaryMeans_IdAuxMean",
                table: "SubjectAuxMeans",
                column: "IdAuxMean",
                principalTable: "AuxiliaryMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAuxMeans_Subjects_IdSub",
                table: "SubjectAuxMeans",
                column: "IdSub",
                principalTable: "Subjects",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomRestrictions_ClassRooms_IdClassRoom",
                table: "ClassRoomRestrictions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomRestrictions_Restrictions_IdRest",
                table: "ClassRoomRestrictions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomTechMeans_ClassRooms_IdClassRoom",
                table: "ClassRoomTechMeans");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomTechMeans_TechnologicalMeans_IdTechMean",
                table: "ClassRoomTechMeans");

            migrationBuilder.DropForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_ProfessorStudentSubjects_IdProfStudSub",
                table: "SecretaryProfessorStudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_Secretaries_IdSec",
                table: "SecretaryProfessorStudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_IdStud",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_IdSub",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAuxMeans_AuxiliaryMeans_IdAuxMean",
                table: "SubjectAuxMeans");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAuxMeans_Subjects_IdSub",
                table: "SubjectAuxMeans");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomRestrictions_ClassRooms_IdRest",
                table: "ClassRoomRestrictions",
                column: "IdRest",
                principalTable: "ClassRooms",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomRestrictions_Restrictions_IdClassRoom",
                table: "ClassRoomRestrictions",
                column: "IdClassRoom",
                principalTable: "Restrictions",
                principalColumn: "IdRes",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomTechMeans_ClassRooms_IdTechMean",
                table: "ClassRoomTechMeans",
                column: "IdTechMean",
                principalTable: "ClassRooms",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomTechMeans_TechnologicalMeans_IdClassRoom",
                table: "ClassRoomTechMeans",
                column: "IdClassRoom",
                principalTable: "TechnologicalMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_ProfessorStudentSubjects_IdSec",
                table: "SecretaryProfessorStudentSubjects",
                column: "IdSec",
                principalTable: "ProfessorStudentSubjects",
                principalColumn: "IdProfStudSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_Secretaries_IdProfStudSub",
                table: "SecretaryProfessorStudentSubjects",
                column: "IdProfStudSub",
                principalTable: "Secretaries",
                principalColumn: "IdS",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_IdSub",
                table: "StudentSubjects",
                column: "IdSub",
                principalTable: "Students",
                principalColumn: "IdStud",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_IdStud",
                table: "StudentSubjects",
                column: "IdStud",
                principalTable: "Subjects",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAuxMeans_AuxiliaryMeans_IdSub",
                table: "SubjectAuxMeans",
                column: "IdSub",
                principalTable: "AuxiliaryMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAuxMeans_Subjects_IdAuxMean",
                table: "SubjectAuxMeans",
                column: "IdAuxMean",
                principalTable: "Subjects",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
