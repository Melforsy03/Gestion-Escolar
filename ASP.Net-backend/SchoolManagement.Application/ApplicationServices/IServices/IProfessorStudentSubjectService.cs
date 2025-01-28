using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IProfessorStudentSubjectService
    {
        Task<ProfessorStudentSubjectResponseDto> CreateProfessorStudentSubjectAsync(ProfessorStudentSubjectDto professorStudentSubjectDto);
        Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectAsync();
        Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectByUserNameAsync(string UserName);

        Task<PSSResponseGetSubjects> GetSubjectsOfProfessorAsync(string UserName);
        Task<PSSResponseGetStudents> GetStudentsForSubjectAsync(int subjectId);
    }
}
