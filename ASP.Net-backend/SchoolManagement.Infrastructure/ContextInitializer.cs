using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchoolManagement.Domain.Role;
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

        public ContextInitializer(ILogger<ContextInitializer> logger, Context context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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
                    await _userManager.AddToRolesAsync(superadmin, new[] { superadminRole.Name });
                }
            }
            //!Also we can define here default data.
        }



    }
}

