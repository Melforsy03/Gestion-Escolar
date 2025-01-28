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
        Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectByUserNameAsync(ProfessorStudentSubjectConsultDto professorStudentSubjectConsultDto);

        Task<PSSResponseGetSubjects> GetSubjectsOfProfessorAsync(ProfessorStudentSubjectConsultDto professorStudentSubjectConsultDto);
        Task<PSSResponseGetStudents> GetStudentsForSubjectAsync(int subjectId);
    }
}
