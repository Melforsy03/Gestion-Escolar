using SchoolManagement.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Professor
    {
        public int IdProf { get; set; } = 0;
        public string UserId { get; set; }
        public string NameProf { get; set; } = string.Empty;
        public string Contract { get; set; } = string.Empty;
        public int Salary { get; set; } = 0;
        public bool IsDean { get; set; } = false;
        public int LaboralExperience { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
