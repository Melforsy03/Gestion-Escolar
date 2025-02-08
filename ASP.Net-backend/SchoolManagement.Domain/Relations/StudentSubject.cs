using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class StudentSubject
    {
        public int IdStudSub { get; set; } = 0;
        public int IdStud { get; set; } = 0;
        public Student Student { get; set; } = new Student();
        public int IdSub { get; set; } = 0;
        public Subject Subject { get; set; } = new Subject();
        public int NJAbsents { get; set; } = 0;

        public List<Professor> Professors { get; set; } = new List<Professor> { };
        
        

      

    }
}
