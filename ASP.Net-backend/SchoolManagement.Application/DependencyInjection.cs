using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Services;
using SchoolManagement.Application.Authentication;
using SchoolManagement.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text; 
using System.Threading.Tasks;


namespace SchoolManagement.Application
{
    public static class DependencyInjection
    {

        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.Configure<JwtSettings>(configurationManager.GetSection(JwtSettings.SECTION_NAME));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IAuxiliaryMeansService, AuxiliaryMeansService>();
            services.AddScoped<IClassRoomService, ClassRoomService>();
            services.AddScoped<IClassRoomRestrictionService, ClassRoomRestrictionService>();
            services.AddScoped<IClassRoomTechMeanService, ClassRoomTechMeanService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IProfessorSubjectService, ProfessorSubjectService>();
            services.AddScoped<IProfessorStudentSubjectService, ProfessorStudentSubjectService>();
            services.AddScoped<IProfStudSubCourseService, ProfStudSubCourseService>();
            services.AddScoped<IRestrictionService, RestrictionService>();
            services.AddScoped<ISecretaryService, SecretaryService>();
            services.AddScoped<ISecretaryProfessorStudentSubjectService, SecretaryProfessorStudentSubjectService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentSubjectService, StudentSubjectService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITechnologicalMeansService, TechnologicalMeansService>();


           services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configurationManager.GetSection("JwtSettings:Issuer").Value,
                    ValidAudience = configurationManager.GetSection("JwtSettings:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager.GetSection("JwtSettings:Secret").Value!))
                };  
            });  
        }
    }
}
