using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class ProfessorStudentSubjectDto
    {
        public int IdProfStudSub { get; set; }
        public int IdProf {  get; set; }
        public int IdStudSub { get; set; }
        public float StudentGrades { get; set; }
    }
}
