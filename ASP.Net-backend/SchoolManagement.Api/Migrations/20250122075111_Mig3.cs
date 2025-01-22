using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class Mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomRestriction_ClassRoom_IdRest",
                table: "ClassRoomRestriction");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomRestriction_Restriction_IdClassRoom",
                table: "ClassRoomRestriction");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomTechMean_ClassRoom_IdTechMean",
                table: "ClassRoomTechMean");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomTechMean_TechnologicalMeans_IdClassRoom",
                table: "ClassRoomTechMean");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenance_AuxiliaryMeans_IdAuxMean",
                table: "Maintenance");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenance_TechnologicalMeans_IdTechMean",
                table: "Maintenance");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Student_StudentIdStud",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorStudentSubject_Professor_IdProf",
                table: "ProfessorStudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorStudentSubject_StudentSubject_IdStudSub",
                table: "ProfessorStudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubject_Professor_IdSub",
                table: "ProfessorSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubject_Subject_IdProf",
                table: "ProfessorSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfStudSubCourse_Course_IdCourse",
                table: "ProfStudSubCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfStudSubCourse_Professor_IdProf",
                table: "ProfStudSubCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfStudSubCourse_Student_IdStud",
                table: "ProfStudSubCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfStudSubCourse_Subject_IdSub",
                table: "ProfStudSubCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_SecretaryProfessorStudentSubject_ProfessorStudentSubject_IdSec",
                table: "SecretaryProfessorStudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SecretaryProfessorStudentSubject_Secretary_IdProfStudSub",
                table: "SecretaryProfessorStudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_IdC",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Student_IdSub",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Subject_IdStud",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_ClassRoom_IdClassRoom",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAuxMean_AuxiliaryMeans_IdSub",
                table: "SubjectAuxMean");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAuxMean_Subject_IdAuxMean",
                table: "SubjectAuxMean");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectAuxMean",
                table: "SubjectAuxMean");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SecretaryProfessorStudentSubject",
                table: "SecretaryProfessorStudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Secretary",
                table: "Secretary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restriction",
                table: "Restriction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfStudSubCourse",
                table: "ProfStudSubCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorSubject",
                table: "ProfessorSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorStudentSubject",
                table: "ProfessorStudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professor",
                table: "Professor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maintenance",
                table: "Maintenance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRoomTechMean",
                table: "ClassRoomTechMean");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRoomRestriction",
                table: "ClassRoomRestriction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRoom",
                table: "ClassRoom");

            migrationBuilder.RenameTable(
                name: "SubjectAuxMean",
                newName: "SubjectAuxMeans");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "StudentSubject",
                newName: "StudentSubjects");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "SecretaryProfessorStudentSubject",
                newName: "SecretaryProfessorStudentSubjects");

            migrationBuilder.RenameTable(
                name: "Secretary",
                newName: "Secretaries");

            migrationBuilder.RenameTable(
                name: "Restriction",
                newName: "Restrictions");

            migrationBuilder.RenameTable(
                name: "ProfStudSubCourse",
                newName: "ProfStudSubCourses");

            migrationBuilder.RenameTable(
                name: "ProfessorSubject",
                newName: "ProfessorSubjects");

            migrationBuilder.RenameTable(
                name: "ProfessorStudentSubject",
                newName: "ProfessorStudentSubjects");

            migrationBuilder.RenameTable(
                name: "Professor",
                newName: "Professors");

            migrationBuilder.RenameTable(
                name: "Maintenance",
                newName: "Maintenances");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "ClassRoomTechMean",
                newName: "ClassRoomTechMeans");

            migrationBuilder.RenameTable(
                name: "ClassRoomRestriction",
                newName: "ClassRoomRestrictions");

            migrationBuilder.RenameTable(
                name: "ClassRoom",
                newName: "ClassRooms");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAuxMean_IdSub",
                table: "SubjectAuxMeans",
                newName: "IX_SubjectAuxMeans_IdSub");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAuxMean_IdAuxMean",
                table: "SubjectAuxMeans",
                newName: "IX_SubjectAuxMeans_IdAuxMean");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_IdClassRoom",
                table: "Subjects",
                newName: "IX_Subjects_IdClassRoom");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubject_IdSub",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_IdSub");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubject_IdStud",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_IdStud");

            migrationBuilder.RenameIndex(
                name: "IX_Student_IdC",
                table: "Students",
                newName: "IX_Students_IdC");

            migrationBuilder.RenameIndex(
                name: "IX_SecretaryProfessorStudentSubject_IdSec",
                table: "SecretaryProfessorStudentSubjects",
                newName: "IX_SecretaryProfessorStudentSubjects_IdSec");

            migrationBuilder.RenameIndex(
                name: "IX_SecretaryProfessorStudentSubject_IdProfStudSub",
                table: "SecretaryProfessorStudentSubjects",
                newName: "IX_SecretaryProfessorStudentSubjects_IdProfStudSub");

            migrationBuilder.RenameIndex(
                name: "IX_ProfStudSubCourse_IdSub",
                table: "ProfStudSubCourses",
                newName: "IX_ProfStudSubCourses_IdSub");

            migrationBuilder.RenameIndex(
                name: "IX_ProfStudSubCourse_IdStud",
                table: "ProfStudSubCourses",
                newName: "IX_ProfStudSubCourses_IdStud");

            migrationBuilder.RenameIndex(
                name: "IX_ProfStudSubCourse_IdProf",
                table: "ProfStudSubCourses",
                newName: "IX_ProfStudSubCourses_IdProf");

            migrationBuilder.RenameIndex(
                name: "IX_ProfStudSubCourse_IdCourse",
                table: "ProfStudSubCourses",
                newName: "IX_ProfStudSubCourses_IdCourse");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorSubject_IdSub",
                table: "ProfessorSubjects",
                newName: "IX_ProfessorSubjects_IdSub");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorSubject_IdProf",
                table: "ProfessorSubjects",
                newName: "IX_ProfessorSubjects_IdProf");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorStudentSubject_IdStudSub",
                table: "ProfessorStudentSubjects",
                newName: "IX_ProfessorStudentSubjects_IdStudSub");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorStudentSubject_IdProf",
                table: "ProfessorStudentSubjects",
                newName: "IX_ProfessorStudentSubjects_IdProf");

            migrationBuilder.RenameIndex(
                name: "IX_Professor_StudentIdStud",
                table: "Professors",
                newName: "IX_Professors_StudentIdStud");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenance_IdTechMean",
                table: "Maintenances",
                newName: "IX_Maintenances_IdTechMean");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenance_IdAuxMean",
                table: "Maintenances",
                newName: "IX_Maintenances_IdAuxMean");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomTechMean_IdTechMean",
                table: "ClassRoomTechMeans",
                newName: "IX_ClassRoomTechMeans_IdTechMean");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomTechMean_IdClassRoom",
                table: "ClassRoomTechMeans",
                newName: "IX_ClassRoomTechMeans_IdClassRoom");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomRestriction_IdRest",
                table: "ClassRoomRestrictions",
                newName: "IX_ClassRoomRestrictions_IdRest");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomRestriction_IdClassRoom",
                table: "ClassRoomRestrictions",
                newName: "IX_ClassRoomRestrictions_IdClassRoom");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectAuxMeans",
                table: "SubjectAuxMeans",
                column: "IdSubAuxMean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "IdSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                column: "IdStudSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "IdStud");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SecretaryProfessorStudentSubjects",
                table: "SecretaryProfessorStudentSubjects",
                column: "IdSecProfStudSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Secretaries",
                table: "Secretaries",
                column: "IdS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restrictions",
                table: "Restrictions",
                column: "IdRes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfStudSubCourses",
                table: "ProfStudSubCourses",
                column: "IdProfStudSubCourse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorSubjects",
                table: "ProfessorSubjects",
                column: "IdProfSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorStudentSubjects",
                table: "ProfessorStudentSubjects",
                column: "IdProfStudSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professors",
                table: "Professors",
                column: "IdProf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances",
                column: "IdM");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "IdC");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRoomTechMeans",
                table: "ClassRoomTechMeans",
                column: "IdClassRoomTech");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRoomRestrictions",
                table: "ClassRoomRestrictions",
                column: "IdClassRoomRest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRooms",
                table: "ClassRooms",
                column: "IdClassR");

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
                name: "FK_Maintenances_AuxiliaryMeans_IdAuxMean",
                table: "Maintenances",
                column: "IdAuxMean",
                principalTable: "AuxiliaryMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_TechnologicalMeans_IdTechMean",
                table: "Maintenances",
                column: "IdTechMean",
                principalTable: "TechnologicalMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Students_StudentIdStud",
                table: "Professors",
                column: "StudentIdStud",
                principalTable: "Students",
                principalColumn: "IdStud");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorStudentSubjects_Professors_IdProf",
                table: "ProfessorStudentSubjects",
                column: "IdProf",
                principalTable: "Professors",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorStudentSubjects_StudentSubjects_IdStudSub",
                table: "ProfessorStudentSubjects",
                column: "IdStudSub",
                principalTable: "StudentSubjects",
                principalColumn: "IdStudSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjects_Professors_IdSub",
                table: "ProfessorSubjects",
                column: "IdSub",
                principalTable: "Professors",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubjects_Subjects_IdProf",
                table: "ProfessorSubjects",
                column: "IdProf",
                principalTable: "Subjects",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfStudSubCourses_Courses_IdCourse",
                table: "ProfStudSubCourses",
                column: "IdCourse",
                principalTable: "Courses",
                principalColumn: "IdC");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfStudSubCourses_Professors_IdProf",
                table: "ProfStudSubCourses",
                column: "IdProf",
                principalTable: "Professors",
                principalColumn: "IdProf");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfStudSubCourses_Students_IdStud",
                table: "ProfStudSubCourses",
                column: "IdStud",
                principalTable: "Students",
                principalColumn: "IdStud");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfStudSubCourses_Subjects_IdSub",
                table: "ProfStudSubCourses",
                column: "IdSub",
                principalTable: "Subjects",
                principalColumn: "IdSub");

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
                name: "FK_Students_Courses_IdC",
                table: "Students",
                column: "IdC",
                principalTable: "Courses",
                principalColumn: "IdC",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ClassRooms_IdClassRoom",
                table: "Subjects",
                column: "IdClassRoom",
                principalTable: "ClassRooms",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Maintenances_AuxiliaryMeans_IdAuxMean",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_TechnologicalMeans_IdTechMean",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Students_StudentIdStud",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorStudentSubjects_Professors_IdProf",
                table: "ProfessorStudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorStudentSubjects_StudentSubjects_IdStudSub",
                table: "ProfessorStudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjects_Professors_IdSub",
                table: "ProfessorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorSubjects_Subjects_IdProf",
                table: "ProfessorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfStudSubCourses_Courses_IdCourse",
                table: "ProfStudSubCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfStudSubCourses_Professors_IdProf",
                table: "ProfStudSubCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfStudSubCourses_Students_IdStud",
                table: "ProfStudSubCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfStudSubCourses_Subjects_IdSub",
                table: "ProfStudSubCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_ProfessorStudentSubjects_IdSec",
                table: "SecretaryProfessorStudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SecretaryProfessorStudentSubjects_Secretaries_IdProfStudSub",
                table: "SecretaryProfessorStudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_IdC",
                table: "Students");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ClassRooms_IdClassRoom",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectAuxMeans",
                table: "SubjectAuxMeans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SecretaryProfessorStudentSubjects",
                table: "SecretaryProfessorStudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Secretaries",
                table: "Secretaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restrictions",
                table: "Restrictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfStudSubCourses",
                table: "ProfStudSubCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorSubjects",
                table: "ProfessorSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorStudentSubjects",
                table: "ProfessorStudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professors",
                table: "Professors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maintenances",
                table: "Maintenances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRoomTechMeans",
                table: "ClassRoomTechMeans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRooms",
                table: "ClassRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRoomRestrictions",
                table: "ClassRoomRestrictions");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "SubjectAuxMeans",
                newName: "SubjectAuxMean");

            migrationBuilder.RenameTable(
                name: "StudentSubjects",
                newName: "StudentSubject");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "SecretaryProfessorStudentSubjects",
                newName: "SecretaryProfessorStudentSubject");

            migrationBuilder.RenameTable(
                name: "Secretaries",
                newName: "Secretary");

            migrationBuilder.RenameTable(
                name: "Restrictions",
                newName: "Restriction");

            migrationBuilder.RenameTable(
                name: "ProfStudSubCourses",
                newName: "ProfStudSubCourse");

            migrationBuilder.RenameTable(
                name: "ProfessorSubjects",
                newName: "ProfessorSubject");

            migrationBuilder.RenameTable(
                name: "ProfessorStudentSubjects",
                newName: "ProfessorStudentSubject");

            migrationBuilder.RenameTable(
                name: "Professors",
                newName: "Professor");

            migrationBuilder.RenameTable(
                name: "Maintenances",
                newName: "Maintenance");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "ClassRoomTechMeans",
                newName: "ClassRoomTechMean");

            migrationBuilder.RenameTable(
                name: "ClassRooms",
                newName: "ClassRoom");

            migrationBuilder.RenameTable(
                name: "ClassRoomRestrictions",
                newName: "ClassRoomRestriction");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_IdClassRoom",
                table: "Subject",
                newName: "IX_Subject_IdClassRoom");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAuxMeans_IdSub",
                table: "SubjectAuxMean",
                newName: "IX_SubjectAuxMean_IdSub");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAuxMeans_IdAuxMean",
                table: "SubjectAuxMean",
                newName: "IX_SubjectAuxMean_IdAuxMean");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_IdSub",
                table: "StudentSubject",
                newName: "IX_StudentSubject_IdSub");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_IdStud",
                table: "StudentSubject",
                newName: "IX_StudentSubject_IdStud");

            migrationBuilder.RenameIndex(
                name: "IX_Students_IdC",
                table: "Student",
                newName: "IX_Student_IdC");

            migrationBuilder.RenameIndex(
                name: "IX_SecretaryProfessorStudentSubjects_IdSec",
                table: "SecretaryProfessorStudentSubject",
                newName: "IX_SecretaryProfessorStudentSubject_IdSec");

            migrationBuilder.RenameIndex(
                name: "IX_SecretaryProfessorStudentSubjects_IdProfStudSub",
                table: "SecretaryProfessorStudentSubject",
                newName: "IX_SecretaryProfessorStudentSubject_IdProfStudSub");

            migrationBuilder.RenameIndex(
                name: "IX_ProfStudSubCourses_IdSub",
                table: "ProfStudSubCourse",
                newName: "IX_ProfStudSubCourse_IdSub");

            migrationBuilder.RenameIndex(
                name: "IX_ProfStudSubCourses_IdStud",
                table: "ProfStudSubCourse",
                newName: "IX_ProfStudSubCourse_IdStud");

            migrationBuilder.RenameIndex(
                name: "IX_ProfStudSubCourses_IdProf",
                table: "ProfStudSubCourse",
                newName: "IX_ProfStudSubCourse_IdProf");

            migrationBuilder.RenameIndex(
                name: "IX_ProfStudSubCourses_IdCourse",
                table: "ProfStudSubCourse",
                newName: "IX_ProfStudSubCourse_IdCourse");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorSubjects_IdSub",
                table: "ProfessorSubject",
                newName: "IX_ProfessorSubject_IdSub");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorSubjects_IdProf",
                table: "ProfessorSubject",
                newName: "IX_ProfessorSubject_IdProf");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorStudentSubjects_IdStudSub",
                table: "ProfessorStudentSubject",
                newName: "IX_ProfessorStudentSubject_IdStudSub");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorStudentSubjects_IdProf",
                table: "ProfessorStudentSubject",
                newName: "IX_ProfessorStudentSubject_IdProf");

            migrationBuilder.RenameIndex(
                name: "IX_Professors_StudentIdStud",
                table: "Professor",
                newName: "IX_Professor_StudentIdStud");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_IdTechMean",
                table: "Maintenance",
                newName: "IX_Maintenance_IdTechMean");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_IdAuxMean",
                table: "Maintenance",
                newName: "IX_Maintenance_IdAuxMean");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomTechMeans_IdTechMean",
                table: "ClassRoomTechMean",
                newName: "IX_ClassRoomTechMean_IdTechMean");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomTechMeans_IdClassRoom",
                table: "ClassRoomTechMean",
                newName: "IX_ClassRoomTechMean_IdClassRoom");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomRestrictions_IdRest",
                table: "ClassRoomRestriction",
                newName: "IX_ClassRoomRestriction_IdRest");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomRestrictions_IdClassRoom",
                table: "ClassRoomRestriction",
                newName: "IX_ClassRoomRestriction_IdClassRoom");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "IdSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectAuxMean",
                table: "SubjectAuxMean",
                column: "IdSubAuxMean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject",
                column: "IdStudSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "IdStud");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SecretaryProfessorStudentSubject",
                table: "SecretaryProfessorStudentSubject",
                column: "IdSecProfStudSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Secretary",
                table: "Secretary",
                column: "IdS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restriction",
                table: "Restriction",
                column: "IdRes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfStudSubCourse",
                table: "ProfStudSubCourse",
                column: "IdProfStudSubCourse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorSubject",
                table: "ProfessorSubject",
                column: "IdProfSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorStudentSubject",
                table: "ProfessorStudentSubject",
                column: "IdProfStudSub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professor",
                table: "Professor",
                column: "IdProf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maintenance",
                table: "Maintenance",
                column: "IdM");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "IdC");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRoomTechMean",
                table: "ClassRoomTechMean",
                column: "IdClassRoomTech");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRoom",
                table: "ClassRoom",
                column: "IdClassR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRoomRestriction",
                table: "ClassRoomRestriction",
                column: "IdClassRoomRest");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomRestriction_ClassRoom_IdRest",
                table: "ClassRoomRestriction",
                column: "IdRest",
                principalTable: "ClassRoom",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomRestriction_Restriction_IdClassRoom",
                table: "ClassRoomRestriction",
                column: "IdClassRoom",
                principalTable: "Restriction",
                principalColumn: "IdRes",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomTechMean_ClassRoom_IdTechMean",
                table: "ClassRoomTechMean",
                column: "IdTechMean",
                principalTable: "ClassRoom",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomTechMean_TechnologicalMeans_IdClassRoom",
                table: "ClassRoomTechMean",
                column: "IdClassRoom",
                principalTable: "TechnologicalMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenance_AuxiliaryMeans_IdAuxMean",
                table: "Maintenance",
                column: "IdAuxMean",
                principalTable: "AuxiliaryMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenance_TechnologicalMeans_IdTechMean",
                table: "Maintenance",
                column: "IdTechMean",
                principalTable: "TechnologicalMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Student_StudentIdStud",
                table: "Professor",
                column: "StudentIdStud",
                principalTable: "Student",
                principalColumn: "IdStud");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorStudentSubject_Professor_IdProf",
                table: "ProfessorStudentSubject",
                column: "IdProf",
                principalTable: "Professor",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorStudentSubject_StudentSubject_IdStudSub",
                table: "ProfessorStudentSubject",
                column: "IdStudSub",
                principalTable: "StudentSubject",
                principalColumn: "IdStudSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubject_Professor_IdSub",
                table: "ProfessorSubject",
                column: "IdSub",
                principalTable: "Professor",
                principalColumn: "IdProf",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorSubject_Subject_IdProf",
                table: "ProfessorSubject",
                column: "IdProf",
                principalTable: "Subject",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfStudSubCourse_Course_IdCourse",
                table: "ProfStudSubCourse",
                column: "IdCourse",
                principalTable: "Course",
                principalColumn: "IdC");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfStudSubCourse_Professor_IdProf",
                table: "ProfStudSubCourse",
                column: "IdProf",
                principalTable: "Professor",
                principalColumn: "IdProf");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfStudSubCourse_Student_IdStud",
                table: "ProfStudSubCourse",
                column: "IdStud",
                principalTable: "Student",
                principalColumn: "IdStud");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfStudSubCourse_Subject_IdSub",
                table: "ProfStudSubCourse",
                column: "IdSub",
                principalTable: "Subject",
                principalColumn: "IdSub");

            migrationBuilder.AddForeignKey(
                name: "FK_SecretaryProfessorStudentSubject_ProfessorStudentSubject_IdSec",
                table: "SecretaryProfessorStudentSubject",
                column: "IdSec",
                principalTable: "ProfessorStudentSubject",
                principalColumn: "IdProfStudSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecretaryProfessorStudentSubject_Secretary_IdProfStudSub",
                table: "SecretaryProfessorStudentSubject",
                column: "IdProfStudSub",
                principalTable: "Secretary",
                principalColumn: "IdS",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_IdC",
                table: "Student",
                column: "IdC",
                principalTable: "Course",
                principalColumn: "IdC",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Student_IdSub",
                table: "StudentSubject",
                column: "IdSub",
                principalTable: "Student",
                principalColumn: "IdStud",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Subject_IdStud",
                table: "StudentSubject",
                column: "IdStud",
                principalTable: "Subject",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_ClassRoom_IdClassRoom",
                table: "Subject",
                column: "IdClassRoom",
                principalTable: "ClassRoom",
                principalColumn: "IdClassR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAuxMean_AuxiliaryMeans_IdSub",
                table: "SubjectAuxMean",
                column: "IdSub",
                principalTable: "AuxiliaryMeans",
                principalColumn: "IdMean",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAuxMean_Subject_IdAuxMean",
                table: "SubjectAuxMean",
                column: "IdAuxMean",
                principalTable: "Subject",
                principalColumn: "IdSub",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
