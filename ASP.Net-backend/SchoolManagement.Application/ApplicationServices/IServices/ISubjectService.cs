using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ISubjectService
    {
        Task<SubjectDto> CreateSubjectAsync(SubjectDto subjectDto);
        Task<SubjectDto> UpdateSubjectAsync(SubjectDto subjectDto);
        Task<IEnumerable<SubjectDto>> ListSubjectAsync();
        Task<SubjectDto> DeleteSubjectByIdAsync(int subjectDto);
    }
}
