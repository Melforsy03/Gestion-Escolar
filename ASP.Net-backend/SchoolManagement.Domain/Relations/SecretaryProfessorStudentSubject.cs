using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Relations
{
    public class SecretaryProfessorStudentSubject
    {
        public int IdSecProfStudSub {  get; set; } = 0;
        public int IdSec { get; set; } = 0;
        public Secretary Secretary { get; set; } = new Secretary();
        public int IdProfStudSub { get; set; } = 0;
        public ProfessorStudentSubject Evaluation { get; set; } = new ProfessorStudentSubject();
    }
}
