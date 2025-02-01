using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Domain.Role;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Infrastructure
{
    public class Context : IdentityDbContext<User>
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        //Entities
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<AuxiliaryMeans> AuxiliaryMeans { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Restriction> Restrictions { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TechnologicalMeans> TechnologicalMeans { get; set; }

        //Relations
        public DbSet<ClassRoomRestriction> ClassRoomRestrictions { get; set; }
        public DbSet<ClassRoomTechMean> ClassRoomTechMeans { get; set; }
        public DbSet<ProfessorStudentSubject> ProfessorStudentSubjects { get; set; }
        public DbSet<ProfessorSubject> ProfessorSubjects { get; set; }
        public DbSet<ProfStudSubCourse> ProfStudSubCourses { get; set; }
        public DbSet<SecretaryProfessorStudentSubject> SecretaryProfessorStudentSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<SubjectAuxMean> SubjectAuxMeans { get; set; }
        public DbSet<ProfessorClassRoom> ProfessorClassRooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* modelBuilder.Entity<User>(entity =>
             {
                 entity.HasKey(u => u.Id);
                 entity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                 entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                 entity.Property(u => u.PasswordHash).IsRequired();
             });  */

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.AdminId);
                entity.Property(e => e.AdminName).IsRequired().HasMaxLength(64);
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(64);
                entity.Property(e => e.AdminSalary).IsRequired();
                entity.Property(e => e.IsDeleted);
            }
            );

            modelBuilder.Entity<ProfStudSubCourse>(entity =>
            {
                entity.HasKey(e => e.IdProfStudSubCourse);
                entity.Property(e => e.Evaluation);

                entity.HasOne(prop => prop.Professor).WithMany().HasForeignKey(prop => prop.IdProf).OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(prop => prop.Subject).WithMany().HasForeignKey(prop => prop.IdSub).OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(prop => prop.Student).WithMany().HasForeignKey(prop => prop.IdStud).OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(prop => prop.Course).WithMany().HasForeignKey(prop => prop.IdCourse).OnDelete(DeleteBehavior.NoAction);



            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.IdProf);
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(64);
                entity.Property(e => e.NameProf).IsRequired().HasMaxLength(64);
                entity.Property(e => e.Contract).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Spec).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Salary).IsRequired();
                entity.Property(e => e.IsDean).IsRequired();
                entity.Property(e => e.LaboralExperience).IsRequired();


                //Relacion de profesor con aula
                entity.HasMany(p => p.ClassRooms).WithMany(c => c.Professors).UsingEntity<ProfessorClassRoom>(
                    pc => pc.HasOne(prop => prop.classRoom).WithMany()
                    .HasForeignKey(prop => prop.IdClassR),
                    pc => pc.HasOne(prop => prop.professor).WithMany()
                    .HasForeignKey(prop => prop.IdProf),
                    pc => pc.HasKey(prop => prop.IdProfClass)
                    );

                //Relacion de Profesor con Asignatura
                entity.HasMany(p => p.Subjects).WithMany(sub => sub.Professors).UsingEntity<ProfessorSubject>(
                 p => p.HasOne(prop => prop.Subject).WithMany()
                 .HasForeignKey(prop => prop.IdSub),
                 p => p.HasOne(prop => prop.Professor).WithMany()
                 .HasForeignKey(prop => prop.IdProf),
                 p => p.HasKey(prop => prop.IdProfSub)
                 );


                //Relacion de Profesor con Estudiante en Asignatura
                entity.HasMany(p => p.StudentSubjects).WithMany(stsub => stsub.Professors).UsingEntity<ProfessorStudentSubject>(
                  pss => pss.HasOne(prop => prop.StudentSubject).WithMany()
                  .HasForeignKey(prop => prop.IdStudSub),
                  pss => pss.HasOne(prop => prop.Professor).WithMany()
                  .HasForeignKey(prop => prop.IdProf),
                  pss =>
                  {
                      pss.Property(prop => prop.StudentGrades).HasDefaultValue(0);
                      pss.HasKey(prop => prop.IdProfStudSub);
                  });

            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.IdSub);
                entity.Property(e => e.NameSub).IsRequired().HasMaxLength(16);
                entity.Property(e => e.StudyProgram).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CourseLoad).IsRequired();
                entity.Property(e => e.IsDeleted);
                //Relacion Asignatura con Aula
                entity.HasOne(e => e.classRoom) // Relación uno a muchos
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.IdClassRoom);
                //Relacion de Asignatura con medios auxiliares
                entity.HasMany(s => s.AuxiliaryMeans).WithMany(am => am.Subjects).UsingEntity<SubjectAuxMean>(
                  sam => sam.HasOne(prop => prop.AuxMean).WithMany()
                  .HasForeignKey(prop => prop.IdAuxMean),
                  sam => sam.HasOne(prop => prop.Subject).WithMany()
                  .HasForeignKey(prop => prop.IdSub),
                  sam => sam.HasKey(prop => prop.IdSubAuxMean)
                  );


            });

            modelBuilder.Entity<Restriction>(entity =>
            {
                entity.HasKey(e => e.IdRes);
                entity.Property(e => e.NameRes).IsRequired().HasMaxLength(16);
                entity.Property(e => e.IsDeleted).IsRequired();

            });

            modelBuilder.Entity<Secretary>(entity =>
            {
                entity.HasKey(e => e.IdS);
                entity.Property(e => e.NameS).IsRequired().HasMaxLength(64);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.SalaryS).IsRequired();
                //Relacion de secretario con evaluaciones(profesor con estudiante en asignatura)
                entity.HasMany(s => s.Evaluations).WithMany(ev => ev.Secretaries).UsingEntity<SecretaryProfessorStudentSubject>(
                    sev => sev.HasOne(prop => prop.Evaluation).WithMany()
                    .HasForeignKey(prop => prop.IdProfStudSub),
                    sev => sev.HasOne(prop => prop.Secretary).WithMany()
                    .HasForeignKey(prop => prop.IdSec),
                    sev => sev.HasKey(prop => prop.IdSecProfStudSub)
                    );

            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.IdC);
                entity.Property(e => e.IsDeleted).IsRequired();
                entity.Property(e => e.CourseName);

            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.HasKey(e => e.IdM);
                entity.Property(e => e.MaintenanceDate).IsRequired();
                entity.Property(e => e.Cost).IsRequired();
                entity.Property(e => e.IsDeleted).IsRequired();
                entity.Property(e => e.typeOfMean).IsRequired();
                //Relacion de Mantenimiento con medios auxiliares
                entity.Property(e => e.IdAuxMean).IsRequired();

                //Relacion de mantenimiento con medios teconologicos
                entity.Property(e => e.IdTechMean).IsRequired();

            });

            modelBuilder.Entity<TechnologicalMeans>(entity =>
            {
                entity.HasKey(e => e.IdMean);
                entity.Property(e => e.NameMean).IsRequired().HasMaxLength(32);
                entity.Property(e => e.State).IsRequired().HasMaxLength(32);
                entity.Property(e => e.isActive);
                entity.Property(e => e.isAviable);
                entity.Property(e => e.isDeleted);
            });

            modelBuilder.Entity<AuxiliaryMeans>(entity =>
            {
                entity.HasKey(e => e.IdMean);
                entity.Property(e => e.NameMean).IsRequired().HasMaxLength(32);
                entity.Property(e => e.State).IsRequired().HasMaxLength(32);
                entity.Property(e => e.isActive);
                entity.Property(e => e.isAviable);
                entity.Property(e => e.isDeleted);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(16);

            });

            modelBuilder.Entity<ClassRoom>(entity =>
            {
                entity.HasKey(e => e.IdClassR);
                entity.Property(e => e.IsAviable).IsRequired();
                entity.Property(e => e.Location).IsRequired();
                entity.Property(e => e.IsDeleted).IsRequired();
                //Relacion aula con restricciones
                entity.HasMany(c => c.Restrictions).WithMany(r => r.ClassRooms).UsingEntity<ClassRoomRestriction>(
                    cr => cr.HasOne(prop => prop.Restriction).WithMany()
                    .HasForeignKey(prop => prop.IdRest),
                    cr => cr.HasOne(prop => prop.ClassRoom).WithMany()
                    .HasForeignKey(prop => prop.IdClassRoom),
                    cr => cr.HasKey(prop => prop.IdClassRoomRest)
                    );
                //Relacion aula con medios tecnologicos
                entity.HasMany(c => c.TechnologicalMeans).WithMany(r => r.ClassRooms).UsingEntity<ClassRoomTechMean>(
                    cr => cr.HasOne(prop => prop.TechnologicalMeans).WithMany()
                    .HasForeignKey(prop => prop.IdTechMean),
                    cr => cr.HasOne(prop => prop.ClassRoom).WithMany()
                    .HasForeignKey(prop => prop.IdClassRoom),
                    cr => cr.HasKey(prop => prop.IdClassRoomTech)
                    );
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStud);
                entity.Property(e => e.NameStud).IsRequired().HasMaxLength(64);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.EActivity).IsRequired();
                //Relacion Estudiante con Curso
                entity.HasOne(s => s.Course) // Relación uno a muchos
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.IdC);

                //Relacion de Estudiante con Asignatura
                entity.HasMany(st => st.Subjects).WithMany(sub => sub.Students).UsingEntity<StudentSubject>(
                  ss => ss.HasOne(prop => prop.Subject).WithMany()
                  .HasForeignKey(prop => prop.IdSub), ss => ss.HasOne(prop => prop.Student).WithMany()
                  .HasForeignKey(prop => prop.IdStud),
                  ss =>
                  {
                      ss.Property(prop => prop.NJAbsents).HasDefaultValue(0);
                      ss.HasKey(prop => prop.IdStudSub);
                  });
            });

        }

    }
}
