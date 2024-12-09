using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class StudentDto
    {
        public int IdStud { get; set; } = 0;
        public string NameStud { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
        public bool EActivity { get; set; } = false;

        //public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
