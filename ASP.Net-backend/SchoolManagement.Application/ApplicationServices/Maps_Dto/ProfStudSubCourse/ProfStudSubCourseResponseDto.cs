using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfStudSubCourse
{
    public class ProfStudSubCourseResponseDto:ProfStudSubCourseDto
    {
        public int IdProfStudSubCourse { get; set; }
        public string ProfessorName { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public string SubjectName { get; set; }
    }
}
