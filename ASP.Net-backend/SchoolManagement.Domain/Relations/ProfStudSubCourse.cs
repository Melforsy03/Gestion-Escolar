using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class ProfStudSubCourse
    {
        public int IdProf { get; set; }
        public Professor Professor { get; set; }

        public int IdStud { get; set; }
        public Student Student{ get; set; }

        public int IdSub { get; set; }
        public Subject Subject { get; set; }

        public int IdCourse { get; set; }
        public Course Course { get; set; }

        //Recordar que el maximo es 10
        public int Evaluation { get; set; }

    }
}
