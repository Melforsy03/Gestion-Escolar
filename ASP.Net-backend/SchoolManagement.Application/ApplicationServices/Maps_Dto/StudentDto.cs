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
        public int IdStud { get; set; }
        public string NameStud { get; set; } 
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; } 
        public bool EActivity { get; set; } 
        public int IdC { get; set; }

        //public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
