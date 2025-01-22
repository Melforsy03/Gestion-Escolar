using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Student
    {
        public int IdStud { get; set; } = 0;
        public string NameStud { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
        public bool EActivity { get; set; } = false;
        public int IdC = 0;
        public Course Course { get; set; } = new Course();
        public bool IsDeleted { get; set; } = false;
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Professor> Professors { get; set; } = new List<Professor>();
    }
}

