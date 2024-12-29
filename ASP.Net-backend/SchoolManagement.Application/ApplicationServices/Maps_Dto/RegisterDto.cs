using SchoolManagement.Domain.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class RegisterDto: UserDto
    {
        public string? Password { get; set; }
        
        [DefaultValue(Role.Student)]
        public string? role{get; set;} 
    }
}
