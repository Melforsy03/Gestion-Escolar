using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Administrator
    {
        public int AdminId = 0;
        public string AdminName = string.Empty;
        public string AdminLastName = string.Empty;
        public int AdminSalary = 0;
        public bool IsDeleted = false;
        
    }
}
