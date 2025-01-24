using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class SecretaryDto
    {
        public int IdS { get; set; }
        public string NameS { get; set; }
        public string UserId {  get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int SalaryS { get; set; }
    }
}
