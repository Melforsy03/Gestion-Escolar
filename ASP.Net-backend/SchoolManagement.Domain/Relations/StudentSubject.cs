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
        public int IdStud { get; set; }
        public Student Student { get; set; }

        public int IdSub { get; set; }
        public Subject Subject { get; set; }

        public int NJAbsents { get; set; }

        public List<Professor> Professors { get; set; }

    }
}
