using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Professor> Professors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.IdProf);
                entity.Property(e => e.NameProf).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastNameProf).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Contract).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Salary).IsRequired();
                entity.Property(e => e.IsDean).IsRequired();
                entity.Property(e => e.LaboralExperience).IsRequired();
            });

            modelBuilder.Entity<ClassRoom>(entity =>
            {
                entity.HasKey(e => e.IdClassR);
                entity.Property(e => e.IsAviable);
                entity.Property(e => e.Ubication);

            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStud);
                entity.Property(e => e.NameStud).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.EAtcivity);

            });
        }
    }
}
