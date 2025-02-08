using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class ProfessorClassRoom
    {
        public int IdProfClass { get; set; }
        public int IdProf {  get; set; }
        public int IdClassR {  get; set; }
        public Professor professor { get; set; }
        public ClassRoom classRoom { get; set; }

    }
}
