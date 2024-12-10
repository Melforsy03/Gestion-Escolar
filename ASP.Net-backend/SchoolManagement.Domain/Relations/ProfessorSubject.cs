using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class ProfessorSubject
    {
        public int IdProf { get; set; }
        public Professor Professor { get; set; }

        public int IdSub { get; set; }
        public Subject Subject { get; set; }

    }
}
