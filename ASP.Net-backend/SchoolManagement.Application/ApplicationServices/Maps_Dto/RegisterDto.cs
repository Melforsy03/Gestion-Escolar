using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Role;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class RegisterDto : UserDto
    {
        public string? Password { get; set; }
        
        [DefaultValue(Role.Professor)]
        public string? role{get; set;} 
    }
}
