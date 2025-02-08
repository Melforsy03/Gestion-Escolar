using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfraestructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<IIdentityManager, IdentityManager>();
            services.AddScoped<IAuxiliaryMeansRepository, AuxiliaryMeansRepository>();
            services.AddScoped<IClassRoomRepository, ClassRoomRepository>();
            services.AddScoped<IClassRoomRestrictionRepository, ClassRoomRestrictionRepository>();
            services.AddScoped<IClassRoomTechMeanRepository, ClassRoomTechMeanRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IProfessorSubjectRepository, ProfessorSubjectRepository>();
            services.AddScoped<IProfessorStudentSubjectRepository, ProfessorStudentSubjectRepository>();
            services.AddScoped<IProfStudSubCourseRepository, ProfStudSubCourseRepository>();
            services.AddScoped<IRestrictionRepository, RestrictionRepository>();
            services.AddScoped<ISecretaryRepository, SecretaryRepository>();
            services.AddScoped<ISecretaryProfessorStudentSubjectRepository,  SecretaryProfessorStudentSubjectRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITechnologicalMeansRepository, TechnologicalMeansRepository>();
            services.AddScoped<ISubjectAuxMeanRepository, SubjectAuxMeanRepository>();
            services.AddScoped<IProfessorClassRoomRepository, ProfessorClassRoomRepository>();
            services.AddScoped<IProfessorPunishmentRepository, ProfessorPunishmentRepository>();
            services.AddScoped<IProfessorNotificationsRepository, ProfessorNotificationsRepository>();
            services.AddScoped<IMeanNotificationRepository, MeanNotificationRepository>();
            services.AddScoped<ContextInitializer>();
            services.AddAuthentication();

            services
            .AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<Context>();
        }
    }
}
