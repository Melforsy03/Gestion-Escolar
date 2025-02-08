using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject
{
    public class ProfessorStudentSubjectDto
    {   public string UserName { get; set; }
        public int IdSub { get; set; }
        public int IdStud { get; set; }
        public float StudentGrades { get; set; }
    }
}
