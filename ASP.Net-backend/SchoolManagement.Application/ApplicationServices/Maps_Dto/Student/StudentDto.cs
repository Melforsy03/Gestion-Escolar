using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.Student
{
    public class StudentDto
    {
        public string NameStud { get; set; }
        public int Age { get; set; }
        public bool EActivity { get; set; }
        public int IdC { get; set; }

        //public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
