using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.StudentSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IStudentSubjectService
    {
        Task<StudentSubjectResponseDto> CreateStudentSubjectAsync(StudentSubjectDto studentSubjectDto);
        Task<StudentSubjectResponseDto> UpdateStudentSubjectAsync(StudentSubjectResponseDto studentSubjectDto);
        Task<IEnumerable<StudentSubjectResponseDto>> ListStudentSubjectAsync();
    }
}
