using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Professor
    {
        public string userId { get; set; }
        public string Email { get; set; } = null!;
        public int IdProf { get; set; }
        public string NameProf { get; set; }
        public string LastNameProf { get; set; }
        public string Contract { get; set; }
        public int Salary { get; set; }
        public bool IsDean { get; set; }
        public int LaboralExperience { get; set; }
        public bool IsDeleted { get; set; }
    }
}
