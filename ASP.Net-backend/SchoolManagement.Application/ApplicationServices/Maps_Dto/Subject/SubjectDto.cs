using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.Subject
{
    public class SubjectDto
    {
        public string NameSub { get; set; } = string.Empty;
        public string StudyProgram { get; set; } = string.Empty;
        public int CourseLoad { get; set; } = 0;
        public int IdClassRoom { get; set; } = 0;
    }
}
