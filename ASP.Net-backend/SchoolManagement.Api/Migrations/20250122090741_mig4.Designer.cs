﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolManagement.Infrastructure;

#nullable disable

namespace SchoolManagement.Api.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250122090741_mig4")]
    partial class mig4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.AuxiliaryMeans", b =>
                {
                    b.Property<int>("IdMean")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMean"));

                    b.Property<int>("IdMaintenance")
                        .HasColumnType("int");

                    b.Property<string>("NameMean")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("isAviable")
                        .HasColumnType("bit");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("IdMean");

                    b.ToTable("AuxiliaryMeans");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.ClassRoom", b =>
                {
                    b.Property<int>("IdClassR")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClassR"));

                    b.Property<bool>("IsAviable")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdClassR");

                    b.ToTable("ClassRooms");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Course", b =>
                {
                    b.Property<int>("IdC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdC"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdC");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Maintenance", b =>
                {
                    b.Property<int>("IdM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdM"));

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<int>("IdAuxMean")
                        .HasColumnType("int");

                    b.Property<int>("IdTechMean")
                        .HasColumnType("int");

                    b.Property<DateOnly>("MaintenanceDate")
                        .HasColumnType("date");

                    b.Property<int>("typeOfMean")
                        .HasColumnType("int");

                    b.HasKey("IdM");

                    b.HasIndex("IdAuxMean");

                    b.HasIndex("IdTechMean");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Professor", b =>
                {
                    b.Property<int>("IdProf")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProf"));

                    b.Property<string>("Contract")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsDean")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LaboralExperience")
                        .HasColumnType("int");

                    b.Property<string>("LastNameProf")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("NameProf")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<int?>("StudentIdStud")
                        .HasColumnType("int");

                    b.Property<string>("uderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProf");

                    b.HasIndex("StudentIdStud");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Restriction", b =>
                {
                    b.Property<int>("IdRes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRes"));

                    b.Property<string>("NameRes")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("IdRes");

                    b.ToTable("Restrictions");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Secretary", b =>
                {
                    b.Property<int>("IdS")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdS"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastNameS")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("NameS")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("SalaryS")
                        .HasColumnType("int");

                    b.HasKey("IdS");

                    b.ToTable("Secretaries");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Student", b =>
                {
                    b.Property<int>("IdStud")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStud"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<bool>("EActivity")
                        .HasColumnType("bit");

                    b.Property<int>("IdC")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("NameStud")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("IdStud");

                    b.HasIndex("IdC");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Subject", b =>
                {
                    b.Property<int>("IdSub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSub"));

                    b.Property<int>("CourseLoad")
                        .HasColumnType("int");

                    b.Property<int>("IdClassRoom")
                        .HasColumnType("int");

                    b.Property<string>("NameSub")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("StudyProgram")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("IdSub");

                    b.HasIndex("IdClassRoom");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.TechnologicalMeans", b =>
                {
                    b.Property<int>("IdMean")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMean"));

                    b.Property<int>("IdMaintenance")
                        .HasColumnType("int");

                    b.Property<string>("NameMean")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("isAviable")
                        .HasColumnType("bit");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("IdMean");

                    b.ToTable("TechnologicalMeans");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ClassRoomRestriction", b =>
                {
                    b.Property<int>("IdClassRoomRest")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClassRoomRest"));

                    b.Property<int>("IdClassRoom")
                        .HasColumnType("int");

                    b.Property<int>("IdRest")
                        .HasColumnType("int");

                    b.HasKey("IdClassRoomRest");

                    b.HasIndex("IdClassRoom");

                    b.HasIndex("IdRest");

                    b.ToTable("ClassRoomRestrictions");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ClassRoomTechMean", b =>
                {
                    b.Property<int>("IdClassRoomTech")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClassRoomTech"));

                    b.Property<int>("IdClassRoom")
                        .HasColumnType("int");

                    b.Property<int>("IdTechMean")
                        .HasColumnType("int");

                    b.HasKey("IdClassRoomTech");

                    b.HasIndex("IdClassRoom");

                    b.HasIndex("IdTechMean");

                    b.ToTable("ClassRoomTechMeans");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ProfStudSubCourse", b =>
                {
                    b.Property<int>("IdProfStudSubCourse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProfStudSubCourse"));

                    b.Property<int>("Evaluation")
                        .HasColumnType("int");

                    b.Property<int>("IdCourse")
                        .HasColumnType("int");

                    b.Property<int>("IdProf")
                        .HasColumnType("int");

                    b.Property<int>("IdStud")
                        .HasColumnType("int");

                    b.Property<int>("IdSub")
                        .HasColumnType("int");

                    b.HasKey("IdProfStudSubCourse");

                    b.HasIndex("IdCourse");

                    b.HasIndex("IdProf");

                    b.HasIndex("IdStud");

                    b.HasIndex("IdSub");

                    b.ToTable("ProfStudSubCourses");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ProfessorStudentSubject", b =>
                {
                    b.Property<int>("IdProfStudSub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProfStudSub"));

                    b.Property<int>("IdProf")
                        .HasColumnType("int");

                    b.Property<int>("IdStudSub")
                        .HasColumnType("int");

                    b.Property<float>("StudentGrades")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.HasKey("IdProfStudSub");

                    b.HasIndex("IdProf");

                    b.HasIndex("IdStudSub");

                    b.ToTable("ProfessorStudentSubjects");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ProfessorSubject", b =>
                {
                    b.Property<int>("IdProfSub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProfSub"));

                    b.Property<int>("IdProf")
                        .HasColumnType("int");

                    b.Property<int>("IdSub")
                        .HasColumnType("int");

                    b.HasKey("IdProfSub");

                    b.HasIndex("IdProf");

                    b.HasIndex("IdSub");

                    b.ToTable("ProfessorSubjects");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.SecretaryProfessorStudentSubject", b =>
                {
                    b.Property<int>("IdSecProfStudSub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSecProfStudSub"));

                    b.Property<int>("IdProfStudSub")
                        .HasColumnType("int");

                    b.Property<int>("IdSec")
                        .HasColumnType("int");

                    b.HasKey("IdSecProfStudSub");

                    b.HasIndex("IdProfStudSub");

                    b.HasIndex("IdSec");

                    b.ToTable("SecretaryProfessorStudentSubjects");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.StudentSubject", b =>
                {
                    b.Property<int>("IdStudSub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStudSub"));

                    b.Property<int>("IdStud")
                        .HasColumnType("int");

                    b.Property<int>("IdSub")
                        .HasColumnType("int");

                    b.Property<int>("NJAbsents")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("IdStudSub");

                    b.HasIndex("IdStud");

                    b.HasIndex("IdSub");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.SubjectAuxMean", b =>
                {
                    b.Property<int>("IdSubAuxMean")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSubAuxMean"));

                    b.Property<int>("IdAuxMean")
                        .HasColumnType("int");

                    b.Property<int>("IdSub")
                        .HasColumnType("int");

                    b.HasKey("IdSubAuxMean");

                    b.HasIndex("IdAuxMean");

                    b.HasIndex("IdSub");

                    b.ToTable("SubjectAuxMeans");
                });

            modelBuilder.Entity("SchoolManagement.Infrastructure.Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SchoolManagement.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SchoolManagement.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SchoolManagement.Infrastructure.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Maintenance", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.AuxiliaryMeans", "auxMean")
                        .WithMany("maintenances")
                        .HasForeignKey("IdAuxMean")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Entities.TechnologicalMeans", "technologicalMean")
                        .WithMany("maintenances")
                        .HasForeignKey("IdTechMean")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("auxMean");

                    b.Navigation("technologicalMean");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Professor", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.Student", null)
                        .WithMany("Professors")
                        .HasForeignKey("StudentIdStud");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Student", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("IdC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Subject", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.ClassRoom", "classRoom")
                        .WithMany("Subjects")
                        .HasForeignKey("IdClassRoom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("classRoom");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ClassRoomRestriction", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.Restriction", "Restriction")
                        .WithMany()
                        .HasForeignKey("IdClassRoom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Entities.ClassRoom", "ClassRoom")
                        .WithMany()
                        .HasForeignKey("IdRest")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassRoom");

                    b.Navigation("Restriction");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ClassRoomTechMean", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.TechnologicalMeans", "TechnologicalMeans")
                        .WithMany()
                        .HasForeignKey("IdClassRoom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Entities.ClassRoom", "ClassRoom")
                        .WithMany()
                        .HasForeignKey("IdTechMean")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassRoom");

                    b.Navigation("TechnologicalMeans");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ProfStudSubCourse", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("IdCourse")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Entities.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("IdProf")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("IdStud")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("IdSub")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Professor");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ProfessorStudentSubject", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("IdProf")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Relations.StudentSubject", "StudentSubject")
                        .WithMany()
                        .HasForeignKey("IdStudSub")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");

                    b.Navigation("StudentSubject");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.ProfessorSubject", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("IdProf")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Entities.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("IdSub")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.SecretaryProfessorStudentSubject", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.Secretary", "Secretary")
                        .WithMany()
                        .HasForeignKey("IdProfStudSub")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Relations.ProfessorStudentSubject", "Evaluation")
                        .WithMany()
                        .HasForeignKey("IdSec")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evaluation");

                    b.Navigation("Secretary");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.StudentSubject", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("IdStud")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("IdSub")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Relations.SubjectAuxMean", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("IdAuxMean")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Entities.AuxiliaryMeans", "AuxMean")
                        .WithMany()
                        .HasForeignKey("IdSub")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuxMean");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.AuxiliaryMeans", b =>
                {
                    b.Navigation("maintenances");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.ClassRoom", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Course", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.Student", b =>
                {
                    b.Navigation("Professors");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Entities.TechnologicalMeans", b =>
                {
                    b.Navigation("maintenances");
                });
#pragma warning restore 612, 618
        }
    }
}
