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
        private readonly ICourseRepository _courseRepository;
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly ITechnologicalMeansRepository _technologicalMeansRepository;
        private readonly ISubjectRepository _subjectRepository;

        public ContextInitializer(ITechnologicalMeansRepository technologicalMeansRepository, IClassRoomRepository classRoomRepository, ICourseRepository courseRepository, IStudentRepository studentRepository, ILogger<ContextInitializer> logger, Context context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _classRoomRepository = classRoomRepository;
            _technologicalMeansRepository = technologicalMeansRepository;
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
                _context.Professors.Add(new Domain.Entities.Professor { IsDean = true , NameProf="Pepe Gonzalez", UserId = superadmin.Id, LaboralExperience = 15, Salary = 10000, IsDeleted = false, Contract = "Tiempo Completo"});
            }


            //!Also we can define here default data.
            //Profesores

            var user1 = new User { UserName = "Pepe0"};

            if (_userManager.Users.All(u => u.UserName != user1.UserName))
            {
                await _userManager.CreateAsync(user1, "Pepe0password1*");
                if (!string.IsNullOrWhiteSpace(professorRole.Name))
                {
                    await _userManager.AddToRoleAsync(user1, professorRole.Name);
                }
                _context.Professors.Add(new Domain.Entities.Professor { IsDean = false, NameProf = "Pepe0 Gonzalez", UserId = user1.Id, LaboralExperience = 5, Salary = 10000, IsDeleted = false, Contract = "Tiempo Completo" });
            }

            var user2 = new User { UserName = "Pepe2" };

            if (_userManager.Users.All(u => u.UserName != user2.UserName))
            {
                await _userManager.CreateAsync(user2, "Pepe2password*");
                if (!string.IsNullOrWhiteSpace(professorRole.Name))
                {
                    await _userManager.AddToRoleAsync(user2, professorRole.Name);
                }
                _context.Professors.Add(new Domain.Entities.Professor { IsDean = false, NameProf = "Pepe2 Gonzalez", UserId = user2.Id, LaboralExperience = 5, Salary = 10000, IsDeleted = false, Contract = "Tiempo Completo" });
            }

            var user3 = new User { UserName = "Pepe3" };

            if (_userManager.Users.All(u => u.UserName != user3.UserName))
            {
                await _userManager.CreateAsync(user3, "Pepe3password*");
                if (!string.IsNullOrWhiteSpace(professorRole.Name))
                {
                    await _userManager.AddToRoleAsync(user3, professorRole.Name);
                }
                _context.Professors.Add(new Domain.Entities.Professor
                {
                    IsDean = false,
                    NameProf = "Pepe3 Gonzalez",
                    UserId = user3.Id,
                    LaboralExperience = 5,
                    Salary = 10000,
                    IsDeleted = false,
                    Contract = "Tiempo Completo"
                });
            }

            var user4 = new User { UserName = "Pepe4" };

            if (_userManager.Users.All(u => u.UserName != user4.UserName))
            {
                await _userManager.CreateAsync(user4, "Pepe4password*");
                if (!string.IsNullOrWhiteSpace(professorRole.Name))
                {
                    await _userManager.AddToRoleAsync(user4, professorRole.Name);
                }
                _context.Professors.Add(new Domain.Entities.Professor
                {
                    IsDean = false,
                    NameProf = "Pepe4 Gonzalez",
                    UserId = user4.Id,
                    LaboralExperience = 5,
                    Salary = 10000,
                    IsDeleted = false,
                    Contract = "Tiempo Completo"
                });
            }

            var user5 = new User { UserName = "Pepe5" };

            if (_userManager.Users.All(u => u.UserName != user5.UserName))
            {
                await _userManager.CreateAsync(user5, "Pepe5password*");
                if (!string.IsNullOrWhiteSpace(professorRole.Name))
                {
                    await _userManager.AddToRoleAsync(user1, professorRole.Name);
                }
                _context.Professors.Add(new Domain.Entities.Professor
                {
                    IsDean = false,
                    NameProf = "Pepe5 Gonzalez",
                    UserId = user5.Id,
                    LaboralExperience = 5,
                    Salary = 10000,
                    IsDeleted = false,
                    Contract = "Tiempo Completo"
                });
            }
            //Medios Tecnologicos
            var medio1 = new TechnologicalMeans();
            var medio2 = new TechnologicalMeans();
            var medio3 = new TechnologicalMeans();
            if(_context.TechnologicalMeans.Count() == 0)
            {
                medio1.State = "ok";
                medio1.NameMean = "medio1";
                await _technologicalMeansRepository.CreateAsync(medio1);
                medio2.State = "ok";
                medio2.NameMean = "medio2";
                await _technologicalMeansRepository.CreateAsync(medio2);
                medio3.State = "ok";
                medio3.State = "medio3";
            }



            //Aulas y materias
            var classRoom1 = new ClassRoom();
            var classRoom2 = new ClassRoom();

            var subject1 = new Subject();
            var subject2 = new Subject();

            if (_context.ClassRooms.Count() == 0)
            {
                
                classRoom1.Location = "A";
                
                classRoom2.Location = "B";
                await _classRoomRepository.CreateAsync(classRoom1);
                await _classRoomRepository.CreateAsync(classRoom2);
               
                //subject1.CourseLoad = 10;
                //subject1.NameSub = "subject1";
                //subject1.classRoom = 

                //                subject2.CourseLoad = 10;
                //              subject2.NameSub = "subject2";
                //            subject2.classRoom = 

                //          _subjectRepository.CreateAsync(subject1).Wait();

                //        _subjectRepository.CreateAsync(subject2).Wait();


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
            _context.SaveChanges();
            //Estudiantes 

            var user6 = new User { UserName = "Pepe6" };

            if (_userManager.Users.All(u => u.UserName != user6.UserName))
            {
                await _userManager.CreateAsync(user6, "Pepe6password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user6, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe6",
                    Age = 5,
                    EActivity = true,
                    UserId = user6.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();

            var user7 = new User { UserName = "Pepe7" };

            if (_userManager.Users.All(u => u.UserName != user7.UserName))
            {
                await _userManager.CreateAsync(user7, "Pepe7password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user7, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe7",
                    Age = 5,
                    EActivity = true,
                    UserId = user7.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();

            var user8 = new User { UserName = "Pepe8" };

            if (_userManager.Users.All(u => u.UserName != user8.UserName))
            {
                await _userManager.CreateAsync(user8, "Pepe8password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user8, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe8",
                    Age = 5,
                    EActivity = true,
                    UserId = user8.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();


            var user9 = new User { UserName = "Pepe9" };

            if (_userManager.Users.All(u => u.UserName != user9.UserName))
            {
                await _userManager.CreateAsync(user9, "Pepe9password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user9, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe9",
                    Age = 5,
                    EActivity = true,
                    UserId = user9.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();

            var user10 = new User { UserName = "Pepe10" };

            if (_userManager.Users.All(u => u.UserName != user10.UserName))
            {
                await _userManager.CreateAsync(user10, "Pepe10password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user10, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe10",
                    Age = 5,
                    EActivity = true,
                    UserId = user10.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();

            var user11 = new User { UserName = "Pepe11" };

            if (_userManager.Users.All(u => u.UserName != user11.UserName))
            {
                await _userManager.CreateAsync(user11, "Pepe11password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user11, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe11",
                    Age = 5,
                    EActivity = true,
                    UserId = user11.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();
            var user12 = new User { UserName = "Pepe12" };

            if (_userManager.Users.All(u => u.UserName != user12.UserName))
            {
                await _userManager.CreateAsync(user12, "Pepe12password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user12, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe12",
                    Age = 5,
                    EActivity = true,
                    UserId = user12.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();

            var user13 = new User { UserName = "Pepe13" };

            if (_userManager.Users.All(u => u.UserName != user13.UserName))
            {
                await _userManager.CreateAsync(user13, "Pepe13password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user13, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe13",
                    Age = 5,
                    EActivity = true,
                    UserId = user13.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();

            var user14 = new User { UserName = "Pepe14" };

            if (_userManager.Users.All(u => u.UserName != user14.UserName))
            {
                await _userManager.CreateAsync(user14, "Pepe14password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user14, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe14",
                    Age = 5,
                    EActivity = true,
                    UserId = user14.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();

            var user15 = new User { UserName = "Pepe15" };

            if (_userManager.Users.All(u => u.UserName != user15.UserName))
            {
                await _userManager.CreateAsync(user15, "Pepe15password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user15, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe15",
                    Age = 5,
                    EActivity = true,
                    UserId = user15.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();

            var user16 = new User { UserName = "Pepe16" };

            if (_userManager.Users.All(u => u.UserName != user16.UserName))
            {
                await _userManager.CreateAsync(user16, "Pepe16password*");
                if (!string.IsNullOrWhiteSpace(studentRole.Name))
                {
                    await _userManager.AddToRoleAsync(user16, studentRole.Name);
                }

                var student = new Student
                {
                    NameStud = "Pepe16",
                    Age = 5,
                    EActivity = true,
                    UserId = user16.Id,
                    Course = _courseRepository.GetById(2)

                };
                await _studentRepository.CreateAsync(student);
            }
            _context.SaveChanges();
        }

    }


}













