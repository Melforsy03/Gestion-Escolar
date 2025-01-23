using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class ProfessorDto
    {
        public int IdProf {  get; set; }
        public string NameProf { get; set; }
        public string Contract { get; set; }
        public int Salary { get; set; }
        public bool IsDean { get; set; }
        public int LaboralExperience { get; set; }
    }
}
