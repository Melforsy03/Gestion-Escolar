using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.Administrator
{
    public class AdministratorCreateResponseDto
    {
        public int Id { get; set; }
        public AdministratorDto Administrator { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
