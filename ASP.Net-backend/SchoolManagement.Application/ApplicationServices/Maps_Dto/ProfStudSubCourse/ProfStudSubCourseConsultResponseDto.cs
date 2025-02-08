using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfStudSubCourse
{
    public class ProfStudSubCourseConsultResponseDto
    {
        public int IdProf { get; set; } = 0;
        public string profName { get; set; }
        public int IdStud { get; set; } = 0;
        public int IdSub { get; set; } = 0;
        public string subjectName { get; set; }
        public int IdCourse { get; set; } = 0;
        public string CourseName { get; set; }
    }
}
