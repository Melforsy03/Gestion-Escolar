using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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
        Task<StudentDto> CreateStudentAsync(StudentDto studentDto);
        Task<StudentDto> UpdateStudentAsync(StudentDto studentDto);
        Task<IEnumerable<StudentDto>> ListStudentAsync();
        Task<StudentDto> DeleteStudentByIdAsync(int studentDto);
    }
}
