using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject
{
    public class PSSResponseGiveNote
    {
        public string userName {  get; set; }
        public string subjectName { get; set; }
        public string studentName { get; set; }
        public float studentGrade { get; set; }
    }
}
