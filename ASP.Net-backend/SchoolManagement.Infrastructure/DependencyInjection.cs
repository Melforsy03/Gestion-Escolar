using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfraestructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuxiliaryMeansRepository, AuxiliaryMeansRepository>();
            services.AddScoped<IClassRoomRepository, ClassRoomRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IProfessorSubjectRepository, ProfessorSubjectRepository>();
            services.AddScoped<IRestrictionRepository, RestrictionRepository>();
            services.AddScoped<ISecretaryRepository, SecretaryRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITechnologicalMeansRepository, TechnologicalMeansRepository>();


        }
    }
}
