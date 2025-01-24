using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class AdministratorDto
    {
        public int AdminId { get; set; }
        public string AdminName {  get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int AdminSalary {  get; set; }
    }
}
