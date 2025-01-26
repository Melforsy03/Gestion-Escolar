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
        Task<(int Id, StudentDto student, string UserName, string Password)> CreateStudentAsync(StudentDto studentDto);
        Task<(int Id, StudentDto student)> UpdateStudentAsync((int Id, StudentDto studentDto) studentInfo);
        Task<IEnumerable<(int Id, StudentDto student)>> ListStudentAsync();
        Task<(int Id, StudentDto student)> DeleteStudentByIdAsync(int studentDto);
    }
}
