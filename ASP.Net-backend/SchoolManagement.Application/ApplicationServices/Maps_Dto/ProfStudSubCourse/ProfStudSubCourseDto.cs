using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfStudSubCourse
{
    public class ProfStudSubCourseDto
    {
        public int IdProf { get; set; } = 0;
        public int IdStud { get; set; } = 0;
        public int IdSub { get; set; } = 0;
        public int IdCourse { get; set; } = 0;
        //Recordar que el maximo es 10
        public int Evaluation { get; set; } = 0;

    }
}
