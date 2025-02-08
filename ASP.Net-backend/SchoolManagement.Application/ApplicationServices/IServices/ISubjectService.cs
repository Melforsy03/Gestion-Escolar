using SchoolManagement.Application.ApplicationServices.Maps_Dto.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ISubjectService
    {
        Task<SubjectResponseDto> CreateSubjectAsync(SubjectDto subjectDto);
        Task<SubjectResponseDto> UpdateSubjectAsync(SubjectResponseDto subjectDto);
        Task<IEnumerable<SubjectResponseDto>> ListSubjectAsync();
        Task<SubjectResponseDto> DeleteSubjectByIdAsync(int subjectDto);
    }
}
