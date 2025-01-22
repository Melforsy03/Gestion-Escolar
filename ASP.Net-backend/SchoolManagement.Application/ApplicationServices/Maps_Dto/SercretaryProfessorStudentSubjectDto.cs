using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Maps_Dto
{
    public class SecretaryProfessorStudentSubjectDto
    {
        public int IdSecProfStudSub { get; set; } = 0;
        public int IdSec { get; set; } = 0;
        public int IdProfStudSub { get; set; } = 0;
    }
}
