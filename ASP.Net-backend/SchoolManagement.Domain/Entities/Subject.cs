using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Subject
    {
        public int IdSub { get; set; }
        public string NameSub { get; set; }
        public string StudyProgram { get; set; }
        public int CourseLoad { get; set; }
    }
}
