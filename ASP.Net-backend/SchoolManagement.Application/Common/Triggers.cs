using Microsoft.AspNetCore.Identity;
using SchoolManagement.Infrastructure;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Common
{
    public class Triggers
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public Triggers(Context context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public float GetAverage(List<float> evaluation)
        {
            float a = 0;
            for (int i = 0; i < evaluation.Count; i++)
            {
                a += evaluation[i];
            }
            if (evaluation.Count == 0) return 0;
            return a / evaluation.Count();

        }
        public float GetAverage(List<int> evaluation)
        {
            int a = 0;
            for (int i = 0; i < evaluation.Count; i++)
            {
                a += evaluation[i];
            }
            if (evaluation.Count == 0) return 0;
            return a / evaluation.Count();

        }

        public async void CheckBadMeans()
        {
            var maintenances = _context.Maintenances.Where(m => m.MaintenanceDate >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1)));
            List<int> technologicalMeans = new List<int>();
            List<int> auxiliaryMeans = new List<int>();
           
            foreach(var m in maintenances)
            {
                if(m.typeOfMean == 0)
                {
                    var meanId = _context.TechnologicalMeans.Where(tm => tm.IdMean == m.IdTechMean).FirstOrDefault().IdMean;
                    if (!technologicalMeans.Contains(meanId))
                    {
                        technologicalMeans.Add(meanId);
                    }
                }
                else if(m.typeOfMean == 1)
                {
                    var meanId = _context.AuxiliaryMeans.Where(am => am.IdMean == m.IdAuxMean).FirstOrDefault().IdMean;
                    if (!auxiliaryMeans.Contains(meanId))
                    {
                        auxiliaryMeans.Add(meanId);
                    }
                }
            }

            foreach(var meanId in technologicalMeans)
            {
                if(maintenances.Where(m => m.typeOfMean == 0 && m.IdTechMean == meanId).Count() > 3)
                {
                    _context.MeanNotifications.Add(new Domain.Notifications.MeanNotifications
                    {
                        MeanID = meanId,
                        MeanName = _context.TechnologicalMeans.Where(tm => tm.IdMean == meanId).First().NameMean,
                        MeanType = "TechnologicalMean",
                        BeenSended = false
                    });
                }
            }

            foreach (var meanId in auxiliaryMeans)
            {
                if (maintenances.Where(m => m.typeOfMean == 1 && m.IdAuxMean == meanId).Count() > 3)
                {
                    _context.MeanNotifications.Add(new Domain.Notifications.MeanNotifications
                    {
                        MeanID = meanId,
                        MeanName = _context.AuxiliaryMeans.Where(am => am.IdMean == meanId).First().NameMean,
                        MeanType = "AuxiliaryMean",
                        BeenSended = false

                    });
                }
            }
            _context.SaveChanges();
        }
        public async void CheckBadProfessors(int CourseId)
        {
            if (_context.Courses.Count() <= 5) return;

            var eval = _context.ProfStudSubCourses;

            List<int> BadProfessor = new List<int>();

            for(int i = CourseId - 5; i <= CourseId; i++)
            {
       
                var evals = _context.ProfStudSubCourses.Where(pssc => pssc.IdCourse == i).ToList();
                var prof = _context.ProfStudSubCourses.Where(pssc => pssc.IdCourse == i).GroupBy(pssc => pssc.IdProf).Select(g => g.Key).ToList();
                foreach (var e in prof)
                {
                    if (!BadProfessor.Contains(e) && i == CourseId-5) 
                    {   
                        var grades = _context.ProfStudSubCourses.Where(pssc => pssc.IdProf == e).Select(g => g.Evaluation).ToList();
                        if(GetAverage(grades) < 3) 
                        {
                            BadProfessor.Add(e);
                        }
                    }else if(BadProfessor.Contains(e) && i != CourseId -5) {
                        var grades = _context.ProfStudSubCourses.Where(pssc => pssc.IdProf == e).Select(g => g.Evaluation).ToList();
                        if(GetAverage(grades) >= 3)
                        {
                            BadProfessor.Remove(e);
                        }
                    }
                }

            }

            foreach(var bp in BadProfessor)
            {
                _context.ProfessorNotifications.Add(new Domain.Notifications.ProfessorNotifications {
                    IdProf = bp,
                    BeenSended = false,
                    ProfName = _context.Professors.Where(p => p.IdProf == bp).First().NameProf,
                    professor = _context.Professors.Where(p => p.IdProf == bp).First()                    
                });
            }

            _context.SaveChanges();
           
        }

        public async Task<(User, string)> RegisterUser(string Name, string Role)
        {
            string ReducedName = string.Empty;
            string Password = string.Empty;
            foreach (char a in Name)
            {
                if (a != ' ')
                {
                    ReducedName += a;
                }
                else
                {
                    break;
                }
            }

            string CandidateName = ReducedName;
            int i = 0;



            while (true)
            {
                bool v = _userManager.Users.All(u => u.UserName != CandidateName);
                if (v)
                {
                    break;
                }
                else
                {
                    CandidateName = ReducedName + i;
                }
                i++;
            }


            Password = GenerateStrongPassword(10);
            User User = new User();
            User.UserName = CandidateName;
            await _userManager.CreateAsync(User, Password);
            await _userManager.AddToRoleAsync(User, Role);

            return (User, Password);
        }

        private static string GenerateStrongPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=[]{}|;:,.<>?";

            var random = new Random();
            var passwordChars = new char[length];

            // Asegurar variedad de caracteres
            passwordChars[0] = validChars[random.Next(26)];  // Letra minúscula
            passwordChars[1] = validChars[random.Next(26, 52)];  // Letra mayúscula
            passwordChars[2] = validChars[random.Next(52, 62)];  // Número
            passwordChars[3] = validChars[random.Next(62)];  // Carácter especial

            // Llenar el resto de caracteres
            for (int i = 4; i < length; i++)
            {
                passwordChars[i] = validChars[random.Next(validChars.Length)];
            }

            // Mezclar los caracteres
            return new string(passwordChars.OrderBy(x => random.Next()).ToArray());
        }
    }
}