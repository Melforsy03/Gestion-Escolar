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
        public int IdProfStudSubCourse { get; set; } = 0;
        public int IdProf { get; set; } = 0;
        public Professor Professor { get; set; } = new Professor();

        public int IdStud { get; set; } = 0;
        public Student Student{ get; set; } = new Student();

        public int IdSub { get; set; } = 0;
        public Subject Subject { get; set; } = new Subject();

        public int IdCourse { get; set; } = 0;
        public Course Course { get; set; } = new Course();

        //Recordar que el maximo es 10
        public int Evaluation { get; set; } = 0;

    }
}
