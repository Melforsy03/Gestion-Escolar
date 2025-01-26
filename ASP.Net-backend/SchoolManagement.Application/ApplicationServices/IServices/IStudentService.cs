using SchoolManagement.Application.ApplicationServices.Maps_Dto.Student;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IStudentService
    {
        Task<StudentCreateResponseDto> CreateStudentAsync(StudentDto studentDto);
        Task<StudentResponseDto> UpdateStudentAsync(StudentResponseDto studentInfo);
        Task<IEnumerable<StudentResponseDto>> ListStudentAsync();
        Task<StudentResponseDto> DeleteStudentByIdAsync(int studentDto);
    }
}
