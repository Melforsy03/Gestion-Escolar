using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Domain.Role;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Infrastructure
{
    public static class InitializerExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initializer = scope.ServiceProvider.GetRequiredService<ContextInitializer>();

            await initializer.InitializeAsync();

            await initializer.SeedAsync();
        }
    }

    public class ContextInitializer
    {
        private readonly ILogger<ContextInitializer> _logger;
        private readonly Context _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly ITechnologicalMeansRepository _technologicalMeansRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAuxiliaryMeansRepository _auxiliaryMeansRepository;

        public ContextInitializer(IProfessorRepository professorRepository, IAuxiliaryMeansRepository auxiliaryMeansRepository, ITechnologicalMeansRepository technologicalMeansRepository, IClassRoomRepository classRoomRepository, ICourseRepository courseRepository, IStudentRepository studentRepository, ILogger<ContextInitializer> logger, Context context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _classRoomRepository = classRoomRepository;
            _technologicalMeansRepository = technologicalMeansRepository;
            _professorRepository = professorRepository;
            _auxiliaryMeansRepository = auxiliaryMeansRepository;
        }

        public async Task InitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            //Default Role
            var superadminRole = new IdentityRole(Role.SuperAdmin);
            var professorRole = new IdentityRole(Role.Professor);
            var adminRole = new IdentityRole(Role.Admin);
            var secretaryRole = new IdentityRole(Role.Secretary);
            var studentRole = new IdentityRole(Role.Student);

            if (_roleManager.Roles.All(r => r.Name != superadminRole.Name))
            {
                await _roleManager.CreateAsync(superadminRole);
            }
            if (_roleManager.Roles.All(r => r.Name != professorRole.Name))
            {
                await _roleManager.CreateAsync(professorRole);
            }
            if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
            {
                await _roleManager.CreateAsync(adminRole);
            }
            if (_roleManager.Roles.All(r => r.Name != secretaryRole.Name))
            {
                await _roleManager.CreateAsync(secretaryRole);
            }
            if (_roleManager.Roles.All(r => r.Name != studentRole.Name))
            {
                await _roleManager.CreateAsync(studentRole);
            }

            //Default User
            var superadmin = new User { UserName = "SuperAdmin", Email = "superadmin@localhost" };

            if (_userManager.Users.All(u => u.UserName != superadmin.UserName))
            {
                await _userManager.CreateAsync(superadmin, "Superadminpassword1*");
                if (!string.IsNullOrWhiteSpace(superadminRole.Name))
                {
                    await _userManager.AddToRoleAsync(superadmin, superadminRole.Name);
                }
                _context.Professors.Add(new Domain.Entities.Professor { IsDean = true , Spec = "spec0", NameProf="Pepe Gonzalez", UserId = superadmin.Id, LaboralExperience = 15, Salary = 10000, IsDeleted = false, Contract = "Tiempo Completo"});
            }

            //Administrador
            if(_context.Administrators.Count() == 0)
            {   
                var admin = new User { UserName = "Admin", Email = "admin@localhost" };
                await _userManager.CreateAsync(admin, "Adminpassword1*");
                await _userManager.AddToRoleAsync(admin, adminRole.Name);
                _context.Administrators.Add(new Administrator { AdminName = "PedroAdmin", AdminSalary = 1500, IsDeleted = false, UserId = admin.Id });
            }

            _context.SaveChanges();

            //Secretary
            if (_context.Secretaries.Count() == 0)
            {
                var secretary = new User { UserName = "Secretary", Email = "secretary@localhost" };
                await _userManager.CreateAsync(secretary, "Secretarypassword1*");
                await _userManager.AddToRoleAsync(secretary, secretaryRole.Name);
                _context.Secretaries.Add(new Secretary { NameS = "Armando", SalaryS = 1500, IsDeleted = false , UserId = secretary.Id});
            }
            _context.SaveChanges();

            //Profesores
            if (_context.Professors.Count() < 2)
            {
                for (int i = 1; i < 10; i++)
                {
                    var user = new User { UserName = "pepe" + i };
                    await _userManager.CreateAsync(user, "Pepe0password1*");
                    if (!string.IsNullOrWhiteSpace(professorRole.Name))
                    {
                        await _userManager.AddToRoleAsync(user, professorRole.Name);

                        _context.Professors.Add(new Domain.Entities.Professor { IsDean = false, Spec = "spec" + i, NameProf = "Pepe" + i + " Gonzalez", UserId = user.Id, LaboralExperience = 5, Salary = 10000, IsDeleted = false, Contract = "Tiempo Completo" });
                    }
                }
                _context.SaveChanges();
            }
         
            //Medios Tecnologicos
            
            if(_context.TechnologicalMeans.Count() == 0)
            {
                for(int i = 0; i < 5; i++)
                {

                    var medio = new TechnologicalMeans();
                    medio.State = "ok";
                    medio.NameMean = "medio" + i;
                    for(int j = 0; j < 10; j++)
                    {
                        var medio2 = new TechnologicalMeans();
                        medio2.State = medio.State;
                        medio2.NameMean = medio.NameMean;
                        await _technologicalMeansRepository.CreateAsync(medio2);
                    }
                }
            }

            _context.SaveChanges();
            
            //Medios Auxiliares
            if(_context.AuxiliaryMeans.Count() == 0) 
            { 
                for(int i =0; i < 5; i++)
                {

                    var medioaux = new AuxiliaryMeans();
                    medioaux.NameMean = "auxmedio" + 1;
                    for(int j = 0; j < 10; j++)
                    {
                        var medioaux2 = new AuxiliaryMeans();
                        medioaux2.NameMean = medioaux.NameMean;
                        await _auxiliaryMeansRepository.CreateAsync(medioaux2);
                    }
                }
            }

            _context.SaveChanges();
            
            //Aulas 
            if (_context.ClassRooms.Count() == 0)
            {             

                for(int i = 0; i < 5; i++)
                {

                    var classRoom = new ClassRoom();
                    classRoom.Location = i.ToString();
                    await _classRoomRepository.CreateAsync(classRoom);
                }
            }
            _context.SaveChanges();
            
            //Materias
            if(_context.Subjects.Count() == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    var subject = new Subject();
                    subject.CourseLoad = 10;
                    subject.NameSub = "subject" + i;
                    subject.IdClassRoom = i + 1;
                    subject.IsDeleted = false;
                    subject.StudyProgram = "studyprogram1";
                    subject.classRoom = await _classRoomRepository.GetByIdAsync(subject.IdClassRoom);
                    _context.Subjects.Add(subject);
                }
            }
            
            _context.SaveChanges();
         
            //Cursos
            var course = new Course();
            var course1 = new Course();
            if(_context.Courses.Count() == 0)
            {
                course.CourseName = "2023-2024";
                await _courseRepository.CreateAsync(course);
                course1.CourseName = "2024-2025";
                await _courseRepository.CreateAsync(course1);
            }
         
            //Estudiantes 
            for(int i = 0; i < 500; i++)
            {
                var user = new User { UserName = "pedro"+i};
                if(_userManager.Users.All(u=>u.UserName != user.UserName))
                {
                    await _userManager.CreateAsync(user, "Pedropassword1*");
                    var mycourse = new Course();
                    if (i % 2 == 0) {
                        mycourse = await _courseRepository.GetByIdAsync(1);
                    }
                    else
                    {
                        mycourse = await _courseRepository.GetByIdAsync(2);
                    }

                    var student = new Student
                    {
                        NameStud = "Pedro" + i + "Gonzalez",
                        Age = 15,
                        EActivity = true,
                        UserId = user.Id,
                        Course = mycourse

                    };
                    _context.Students.Add(student);
                }
            }
            _context.SaveChanges();
            
            //Relacion de Aula con Medios Tecnicos
            if(_context.ClassRoomTechMeans.Count() == 0)
            {
                for(int i = 0; i < 5; i++)
                {
                    var crtm = new ClassRoomTechMean();
                    crtm.TechnologicalMeans = await _technologicalMeansRepository.GetByIdAsync(i + 1);
                    crtm.ClassRoom = await _classRoomRepository.GetByIdAsync(i + 1);
                    crtm.IdTechMean = i + 1;
                    crtm.IdClassRoom = i + 1;
                    _context.ClassRoomTechMeans.Add(crtm);
                }
            }
            _context.SaveChanges();

            //Relacion Profesor con Asignatura
            if(_context.ProfessorSubjects.Count() == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    var subject = _context.Subjects.ToList()[i];
                    int j = 2 * (i+1);
                    
                    var professor1 = await _professorRepository.GetByIdAsync(j - 1);
                    var professor2 = await _professorRepository.GetByIdAsync(j);
                    var profSub1 = new ProfessorSubject();
                    var profSub2 = new ProfessorSubject();

                    profSub1.Subject = subject;
                    profSub1.Professor = professor1;
                    profSub1.IdProf = professor1.IdProf;
                    profSub1.IdSub = subject.IdSub;

                    profSub2.Subject = subject;
                    profSub2.Professor = professor2;
                    profSub1.IdProf = professor2.IdProf;
                    profSub1.IdSub = subject.IdSub;

                    _context.ProfessorSubjects.Add(profSub1);

                    _context.ProfessorSubjects.Add(profSub2);

                    
                }
            }
            _context.SaveChanges();

            //Relacion Estudiante con asignatura
            if (_context.StudentSubjects.Count() == 0)
            {
                for(int i = 0; i < 500; i++)
                {
                    var student = _context.Students.ToList()[i];
                    List<Subject> subjects = new List<Subject>();
                    if(i%2 == 0)
                    {
                        subjects = _context.Subjects.ToList().GetRange(0,2);
                    }
                    else
                    {
                        subjects = _context.Subjects.ToList().GetRange(2,2);
                    }
                    for(int j = 0; j < 2; j++) 
                    {

                        var studentSubject = new StudentSubject();
                        studentSubject.Subject = subjects[j];
                        studentSubject.Student = student;
                        studentSubject.IdStud = student.IdStud;
                        studentSubject.IdSub = subjects[j].IdSub;

                        _context.StudentSubjects.Add(studentSubject);

                    }
                }
            }
            _context.SaveChanges();

            //Relacion Materia con medios auxiliares
            if(_context.SubjectAuxMeans.Count() == 0)
            {
                for(int i = 0; i <  5; i ++)
                {
                    var subjectAux = new SubjectAuxMean();
                    subjectAux.Subject = _context.Subjects.ToList()[i];
                    subjectAux.AuxMean = _context.AuxiliaryMeans.ToList()[i];
                    subjectAux.IdSub = subjectAux.Subject.IdSub;
                    subjectAux.IdAuxMean = subjectAux.AuxMean.IdMean;

                    _context.SubjectAuxMeans.Add(subjectAux);

                }
            }

            _context.SaveChanges();

            //Nota
            if(_context.ProfessorStudentSubjects.Count() == 0)
            {
                var professor = _context.Professors.ToList();

                for(int i = 0; i < professor.Count(); i++)
                {
                    var studentSubject = _context.StudentSubjects.Where(ss => ss.IdSub == _context.ProfessorSubjects.Where(ps => ps.IdProf == professor[i].IdProf).First().IdSub).ToList();

                    if (studentSubject.Count() == 0) break;
                    for(int j = 0; j < studentSubject.Count(); j++)
                    {
                        var profStudentSubject = new ProfessorStudentSubject();
                        profStudentSubject.Professor = professor[i];
                        profStudentSubject.StudentSubject = studentSubject[j];
                        profStudentSubject.IdProf = professor[i].IdProf;
                        profStudentSubject.IdStudSub = studentSubject[j].IdSub;
                        profStudentSubject.StudentGrades = 8;

                        _context.ProfessorStudentSubjects.Add(profStudentSubject);
                    }
                    
                }
            }
            _context.SaveChanges();

            
        }

    }


}













