using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;

namespace SchoolManagement.Infrastructure
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Professor> Professor { get; set; }
        public DbSet<AuxiliaryMeans> AuxiliaryMeans { get; set; }
        public DbSet<ClassRoom> ClassRoom { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }
        public DbSet<Restriction> Restrction { get; set; }
        public DbSet<Secretary> Secretary { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<TechnologicalMeans> TechnologicalMeans { get; set; }
        public DbSet<ProfessorSubject> ProfessorSubject { get; set; }
        public DbSet<StudentSubject> StudentSubject { get; set; }
        public DbSet<ProfessorStudentSubject> ProfessorStudentSubject { get; set; }
       // public DbSet<ProfStudSubCourse> ProfStudSubCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

           /* modelBuilder.Entity<ProfStudSubCourse>(entity =>
            {
                entity.HasKey(e => new{e.IdProf, e.IdStud, e.IdSub, e.IdCourse});
            });*/
            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.IdProf);
                entity.Property(e => e.NameProf).IsRequired().HasMaxLength(16);
                entity.Property(e => e.LastNameProf).IsRequired().HasMaxLength(16);
                entity.Property(e => e.Contract).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Salary).IsRequired();
                entity.Property(e => e.IsDean).IsRequired();
                entity.Property(e => e.LaboralExperience).IsRequired();

                //Relacion de Profesor con Asignatura
                entity.HasMany(p => p.Subjects).WithMany(sub => sub.Professors).UsingEntity<ProfessorSubject>(
                 p => p.HasOne(prop => prop.Subject).WithMany()
                 .HasForeignKey(prop => prop.IdProf), 
                 
                 p => p.HasOne(prop => prop.Professor).WithMany()
                 .HasForeignKey(prop => prop.IdSub),
                 p => p.HasKey(prop => new { prop.IdProf, prop.IdSub })
                 );


                //Relacion de Profesor con Estudiante en Asignatura
               entity.HasMany(p => p.StudentSubjects).WithMany(stsub => stsub.Professors).UsingEntity<ProfessorStudentSubject>(
                  pss => pss.HasOne(prop => prop.StudentSubject).WithMany()
                  .HasForeignKey(prop => new { prop.IdStud, prop.IdSub }),
                  pss => pss.HasOne(prop => prop.Professor).WithMany()
                  .HasForeignKey(prop => prop.IdProf),

            pss =>
                  {
                      pss.Property(prop => prop.StudentGrades).HasDefaultValue(0);
                      pss.HasKey(prop => new { prop.IdProf, prop.IdStud, prop.IdSub });
                  });
                
            });
            

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.IdSub);
                entity.Property(e => e.NameSub).IsRequired().HasMaxLength(16);
                entity.Property(e => e.StudyProgram).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CourseLoad).IsRequired();

            });

            modelBuilder.Entity<Restriction>(entity =>
            {
                entity.HasKey(e => e.IdRes);
                entity.Property(e => e.NameRes).IsRequired().HasMaxLength(16);

            });

            modelBuilder.Entity<Secretary>(entity =>
            {
                entity.HasKey(e => e.IdS);
                entity.Property(e => e.NameS).IsRequired().HasMaxLength(16);
                entity.Property(e => e.LastNameS).IsRequired().HasMaxLength(16);
                entity.Property(e => e.SalaryS).IsRequired();

            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.IdC);

            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.HasKey(e => e.IdM);
                entity.Property(e => e.MaintenanceDate).IsRequired();
                entity.Property(e => e.Cost).IsRequired();

            });

            modelBuilder.Entity<TechnologicalMeans>(entity =>
            {
                entity.HasKey(e => e.IdMean);
                entity.Property(e => e.NameMean).IsRequired().HasMaxLength(16);
                entity.Property(e => e.State).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Ammount).IsRequired();

            });

            modelBuilder.Entity<AuxiliaryMeans>(entity =>
            {
                entity.HasKey(e => e.IdMean);
                entity.Property(e => e.NameMean).IsRequired().HasMaxLength(16);
                entity.Property(e => e.State).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Ammount).IsRequired();
                entity.Property(e => e.Type).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<ClassRoom>(entity =>
            {
                entity.HasKey(e => e.IdClassR);
                entity.Property(e => e.IsAviable).IsRequired();
                entity.Property(e => e.Location).IsRequired();

            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStud);
                entity.Property(e => e.NameStud).IsRequired().HasMaxLength(32);
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.EActivity).IsRequired();
                //Relacion Estudiante con Curso
                entity.HasOne(s => s.Course) // Relación uno a muchos
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.IdC);

                //Relacion de Estudiante con Asignatura
                entity.HasMany(st => st.Subjects).WithMany(sub => sub.Students).UsingEntity<StudentSubject>(
                  ss => ss.HasOne(prop => prop.Subject).WithMany()
                  .HasForeignKey(prop => prop.IdStud), ss => ss.HasOne(prop => prop.Student).WithMany()
                  .HasForeignKey(prop => prop.IdSub),
                  ss =>
                  {
                      ss.Property(prop => prop.NJAbsents).HasDefaultValue(0);
                      ss.HasKey(prop => new { prop.IdStud, prop.IdSub });
                  });
                //Agregacion Evalucion
                /*entity.HasMany(st => st.Professors).WithMany(p => p.Students).UsingEntity<ProfStudSubCourse>(
                    pssc => pssc.HasOne(prop => prop.Professor).WithMany()
                    .HasForeignKey(prop => prop.IdStud), pssc => pssc.HasOne(prop => prop.Student).WithMany()
                    .HasForeignKey(prop => prop.IdProf),*/
                    
                  //);

            });

        }

    }
}
