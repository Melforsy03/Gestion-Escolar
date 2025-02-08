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
        public int IdProfSub {  get; set; } = 0;
        public int IdProf { get; set; } = 0;
        public Professor Professor { get; set; } = new Professor();

        public int IdSub { get; set; } = 0;
        public Subject Subject { get; set; } = new Subject();

    }
}
