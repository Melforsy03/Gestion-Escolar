using SchoolManagement.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Secretary
    {
        public int IdS { get; set; } = 0;
        public string NameS { get; set; } = string.Empty;
        public string LastNameS { get; set; } = string.Empty;
        public int SalaryS { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public List<ProfessorStudentSubject> Evaluations { get; set; } = new List<ProfessorStudentSubject>();   
    }
}
