﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Infrastructure.Identity;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IIdentityService
    {
        Task<(string, string)> CreateUserAsync(RegisterDto userDto);
        Task<(bool, string, string)> CheckCredentialsAsync(LoginDto userDto);
        Task<IEnumerable<User>> ListUsersAsync();
    }
}
