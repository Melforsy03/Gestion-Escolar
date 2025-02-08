using Microsoft.AspNetCore.Identity;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Common
{
    public class Triggers
    {
        // Contexto de la base de datos y gestores de usuarios y roles
        private readonly Context _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Constructor que inicializa el contexto y los gestores
        public Triggers(Context context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Método para calcular el promedio de una lista de evaluaciones de tipo float
        public float GetAverage(List<float> evaluation)
        {
            float a = 0; // Variable para almacenar la suma de las evaluaciones
            for (int i = 0; i < evaluation.Count; i++)
            {
                a += evaluation[i]; // Sumar cada evaluación
            }
            if (evaluation.Count == 0) return 0; // Retornar 0 si la lista está vacía
            return a / evaluation.Count(); // Retornar el promedio
        }

        // Método sobrecargado para calcular el promedio de una lista de evaluaciones de tipo int
        public float GetAverage(List<int> evaluation)
        {
            int a = 0; // Variable para almacenar la suma de las evaluaciones
            for (int i = 0; i < evaluation.Count; i++)
            {
                a += evaluation[i]; // Sumar cada evaluación
            }
            if (evaluation.Count == 0) return 0; // Retornar 0 si la lista está vacía
            return a / evaluation.Count(); // Retornar el promedio
        }

        // Método para verificar medios tecnológicos y auxiliares con malas evaluaciones
        public async void CheckBadMeans()
        {
            // Obtener mantenimientos realizados en el último año
            var maintenances = _context.Maintenances.Where(m => m.MaintenanceDate >= DateOnly.FromDateTime(DateTime.Now.AddYears(-1)));
            List<int> technologicalMeans = new List<int>(); // Lista para medios tecnológicos
            List<int> auxiliaryMeans = new List<int>(); // Lista para medios auxiliares

            // Recorrer los mantenimientos y clasificar los medios por tipo
            foreach (var m in maintenances)
            {
                if (m.typeOfMean == 0) // Si es un medio tecnológico
                {
                    var meanId = _context.TechnologicalMeans.Where(tm => tm.IdMean == m.IdTechMean).FirstOrDefault().IdMean;
                    if (!technologicalMeans.Contains(meanId))
                    {
                        technologicalMeans.Add(meanId); // Agregar ID del medio si no está en la lista
                    }
                }
                else if (m.typeOfMean == 1) // Si es un medio auxiliar
                {
                    var meanId = _context.AuxiliaryMeans.Where(am => am.IdMean == m.IdAuxMean).FirstOrDefault().IdMean;
                    if (!auxiliaryMeans.Contains(meanId))
                    {
                        auxiliaryMeans.Add(meanId); // Agregar ID del medio si no está en la lista
                    }
                }
            }

            // Verificar medios tecnológicos con más de 3 mantenimientos y crear notificaciones si es necesario
            foreach (var meanId in technologicalMeans)
            {
                if (maintenances.Where(m => m.typeOfMean == 0 && m.IdTechMean == meanId).Count() > 3)
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

            // Verificar medios auxiliares con más de 3 mantenimientos y crear notificaciones si es necesario
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

            _context.SaveChanges(); // Guardar cambios en la base de datos
        }

        // Método para verificar profesores con malas evaluaciones en un curso específico
        public async void CheckBadProfessors(int CourseId)
        {
            if (_context.Courses.Count() <= 5) return; // No hacer nada si hay menos de 5 cursos

            var eval = _context.ProfStudSubCourses; // Obtener evaluaciones

            List<int> BadProfessor = new List<int>(); // Lista para profesores con malas evaluaciones

            for (int i = CourseId - 5; i <= CourseId; i++)
            {
                var evals = _context.ProfStudSubCourses.Where(pssc => pssc.IdCourse == i).ToList();
                var prof = _context.ProfStudSubCourses.Where(pssc => pssc.IdCourse == i).GroupBy(pssc => pssc.IdProf).Select(g => g.Key).ToList();

                foreach (var e in prof)
                {
                    if (!BadProfessor.Contains(e) && i == CourseId - 5)
                    {
                        var grades = _context.ProfStudSubCourses.Where(pssc => pssc.IdProf == e).Select(g => g.Evaluation).ToList();
                        if (GetAverage(grades) < 3)
                        {
                            BadProfessor.Add(e); // Agregar profesor a la lista si su promedio es menor que 3
                        }
                    }
                    else if (BadProfessor.Contains(e) && i != CourseId - 5)
                    {
                        var grades = _context.ProfStudSubCourses.Where(pssc => pssc.IdProf == e).Select(g => g.Evaluation).ToList();
                        if (GetAverage(grades) >= 3)
                        {
                            BadProfessor.Remove(e); // Remover profesor si su promedio mejora a 3 o más
                        }
                    }
                }
            }

            foreach (var bp in BadProfessor)
            {
                _context.ProfessorNotifications.Add(new Domain.Notifications.ProfessorNotifications
                {
                    IdProf = bp,
                    BeenSended = false,
                    ProfName = _context.Professors.Where(p => p.IdProf == bp).First().NameProf,
                    professor = _context.Professors.Where(p => p.IdProf == bp).First()
                });
            }

            _context.SaveChanges(); // Guardar cambios en la base de datos           
        }

        // Método para registrar un nuevo usuario y asignarle un rol
        public async Task<(User, string)> RegisterUser(string Name, string Role)
        {
            string ReducedName = string.Empty; // Nombre reducido sin espacios
            string Password = string.Empty; // Contraseña generada

            foreach (char a in Name)
            {
                if (a != ' ')
                {
                    ReducedName += a; // Construir nombre reducido eliminando espacios
                }
                else
                {
                    break; // Salir al encontrar un espacio
                }
            }

            string CandidateName = ReducedName;
            int i = 0;

            while (true)
            {
                bool v = _userManager.Users.All(u => u.UserName != CandidateName);
                if (v)
                {
                    break; // Salir si el nombre de usuario es único
                }
                else
                {
                    CandidateName = ReducedName + i; // Incrementar el sufijo numérico si ya existe el nombre de usuario 
                }
                i++;
            }

            Password = GenerateStrongPassword(10); // Generar una contraseña fuerte de 10 caracteres

            User User = new User();
            User.UserName = CandidateName;

            await _userManager.CreateAsync(User, Password); // Crear el usuario con la contraseña generada
            await _userManager.AddToRoleAsync(User, Role); // Asignar rol al usuario

            return (User, Password); // Retornar el usuario creado y su contraseña 
        }

        // Método privado para generar una contraseña fuerte aleatoria
        private static string GenerateStrongPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=[]{}|;:,.<>?";

            var random = new Random();
            var passwordChars = new char[length];

            // Asegurar variedad de caracteres en la contraseña generada.
            passwordChars[0] = validChars[random.Next(26)];  // Letra minúscula.
            passwordChars[1] = validChars[random.Next(26, 52)];  // Letra mayúscula.
            passwordChars[2] = validChars[random.Next(52, 62)];  // Número.
            passwordChars[3] = validChars[random.Next(62)];  // Carácter especial.

            for (int i = 4; i < length; i++)
            {
                passwordChars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(passwordChars.OrderBy(x => random.Next()).ToArray()); // Mezclar y retornar la contraseña generada.
        }

        public  bool CheckName(string Name)
        {
            foreach (char c in Name)
            {
                if (!(char.IsLetter(c) || c == ' ')) // Si encuentra un carácter que no es letra, retorna false
                {
                    return false;
                }


            }
            return true;
        }

        public  bool CheckRange(int start, int end, int input)
        {
            if(input > start && input <= end)
            {
                return true;
            }

            return false;
        }
        public bool CheckRange(int start, int end, float input)
        {
            if (input > start && input <= end)
            {
                return true;
            }

            return false;
        }
    }
}
