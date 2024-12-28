using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class ProfessorStudentSubject
    {
        public int IdProf { get; set; }
        public Professor Professor { get; set; }
        public int IdStud { get; set; }
        public int IdSub {  get; set; }
        public StudentSubject StudentSubject { get; set; }
        public float StudentGrades { get; set; }
    }
}
