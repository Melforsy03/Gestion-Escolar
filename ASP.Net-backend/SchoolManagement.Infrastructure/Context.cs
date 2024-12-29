﻿using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;z

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
                entity.Property(e => e.NameProf).IsRequired().HasMaxLength(16);
                entity.Property(e => e.LastNameProf).IsRequired().HasMaxLength(16);
                entity.Property(e => e.Contract).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Salary).IsRequired();
                entity.Property(e => e.IsDean).IsRequired();
                entity.Property(e => e.LaboralExperience).IsRequired();
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

            });

        }

    }
}
