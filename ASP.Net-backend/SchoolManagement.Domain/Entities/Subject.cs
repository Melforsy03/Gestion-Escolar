using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Subject
    {
        public int IdSub { get; set; } = 0;
        public string NameSub { get; set; } = string.Empty;
        public string StudyProgram { get; set; } = string.Empty;
        public int CourseLoad { get; set; } = 0;
        public int IdClassRoom { get; set; } = 0;
        public ClassRoom classRoom { get; set; } = new ClassRoom();
        public List<Professor> Professors { get; set; } = new List<Professor>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<AuxiliaryMeans> AuxiliaryMeans { get; set; } = new List<AuxiliaryMeans> { };
    }
}
