using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application
{
    public static class DependencyInjection
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAuxiliaryMeansService, AuxiliaryMeansService>();
            services.AddScoped<IClassRoomService, ClassRoomService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IRestrictionService, RestrictionService>();
            services.AddScoped<ISecretaryService, SecretaryService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentSubjectService, StudentSubjectService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITechnologicalMeansService, TechnologicalMeansService>();
        }
    }
}
