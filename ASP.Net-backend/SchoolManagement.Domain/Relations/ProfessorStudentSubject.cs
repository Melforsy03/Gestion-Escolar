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
        public int IdProfStudSub {  get; set; } = 0;
        public int IdProf { get; set; } = 0;
        public Professor Professor { get; set; } = new Professor();
        public int IdStudSub { get; set; } = 0;
        public StudentSubject StudentSubject { get; set; } = new StudentSubject();
        public float StudentGrades { get; set; } = 0;
        public List<Secretary> Secretaries { get; } = new List<Secretary>();
    }
}
