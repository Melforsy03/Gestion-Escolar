using SchoolManagement.Application.ApplicationServices.Maps_Dto.SecretaryProfessorStudentSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ISecretaryProfessorStudentSubjectService
    {
        Task<SecretaryProfessorStudentSubjectResponseDto> CreateSecretaryProfessorStudentSubjectAsync(SecretaryProfessorStudentSubjectDto secretaryDto);
        Task<SecretaryProfessorStudentSubjectResponseDto> UpdateSecretaryProfessorStudentSubjectAsync(SecretaryProfessorStudentSubjectResponseDto secretaryDto);
        Task<IEnumerable<SecretaryProfessorStudentSubjectResponseDto>> ListSecretariesProfessorStudentSubjectsAsync();
    }

}
