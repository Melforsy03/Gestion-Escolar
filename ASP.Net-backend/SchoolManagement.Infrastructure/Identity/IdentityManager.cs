using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Infrastructure.Identity
{
   
    public interface IIdentityManager
    {
        Task<User> CreateUserAsync(User user, string password);
        Task AddRoles(string userId, string role);
        Task<bool> CheckCredentialsAsync(string username, string password);
        Task<bool> IsInRoleAsync(string userId, string role);
        IEnumerable<User> ListUsersAsync();
    }


    public class IdentityManager : IIdentityManager
    {
        private readonly UserManager<User> _userManager;
        public IdentityManager(UserManager<User> userManage)
        {
            _userManager = userManage;
        }
        public async Task<User> CreateUserAsync(User user, string password)
        {
            
            await _userManager.CreateAsync(user, password);
           
            return user;
        }


        public async Task AddRoles(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is not null)
                await _userManager.AddToRoleAsync(user, role);

        }

        public async Task<bool> CheckCredentialsAsync(string username, string password)
        {
            var user = await _userManager.Users
                               .FirstOrDefaultAsync(u => u.UserName!.Equals(username));
           
            if (user is null)
               return false;

            return await _userManager.CheckPasswordAsync(user, password);

        }


        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public IEnumerable<User> ListUsersAsync()
        {
            var users = _userManager.Users;
            return users;
        }
    }
}
