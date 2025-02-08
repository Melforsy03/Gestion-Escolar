using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
namespace SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject
{
    public class PSSResponseGetSubjects
    {   
        public List<Domain.Entities.Subject> subjects {  get; set; }
    }
}
