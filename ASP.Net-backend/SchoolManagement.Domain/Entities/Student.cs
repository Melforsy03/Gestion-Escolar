using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Student
    {
        public int IdStud { get; set; }
        public string NameStud { get; set; }
        public int Age { get; set; }
        public bool EAtcivity { get; set; }
    }
}
