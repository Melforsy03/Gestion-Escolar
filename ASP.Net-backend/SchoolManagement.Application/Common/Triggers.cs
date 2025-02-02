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