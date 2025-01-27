using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class ClassRoomProfessor
    {
        public int IdClassRoomProf { get; set; }
        public int IdClassRoom { get; set; }
        public int IdProf { get; set; }
        public Professor Professors { get; set; }
        public ClassRoom ClassRoom { get; set; }
    }
}
