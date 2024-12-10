using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IProfessorStudentSubjectService
    {
        Task<ProfessorStudentSubjectDto> CreateProfessorStudentSubjectAsync(ProfessorStudentSubjectDto professorStudentSubjectDto);
        Task<ProfessorStudentSubjectDto> UpdateProfessorStudentSubjectAsync(ProfessorStudentSubjectDto professorStudentSubjectDto);
        Task<IEnumerable<ProfessorStudentSubjectDto>> ListProfessorStudentSubjectAsync();
    }
}
