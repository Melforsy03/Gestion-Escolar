using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject
{
    public class ProfessorStudentSubjectResponseDto: ProfessorStudentSubjectDto
    {
        public int IdProfStudSub { get; set; }
        public string studentName { get; set; }
        public string professorName { get; set; }
        public string subjectName { get; set; }
    }
}
