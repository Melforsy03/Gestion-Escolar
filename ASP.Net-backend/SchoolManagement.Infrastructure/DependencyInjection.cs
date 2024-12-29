using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using SchoolManagement.Infrastructure.Identity;


namespace SchoolManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var db = services.AddDbContext<Context>(x =>
                    { x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });
            services.AddScoped<IIdentityManager, IdentityManager>();
            services.AddScoped<IAuxiliaryMeansRepository, AuxiliaryMeansRepository>();
            services.AddScoped<IClassRoomRepository, ClassRoomRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IRestrictionRepository, RestrictionRepository>();
            services.AddScoped<ISecretaryRepository, SecretaryRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITechnologicalMeansRepository, TechnologicalMeansRepository>();  

            services.AddAuthentication();

            services
            .AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<Context>();
    
        }
    }
}
