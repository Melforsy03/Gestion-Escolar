using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfStudSubCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IProfStudSubCourseService
    {
        Task<ProfStudSubCourseResponseDto> CreateProfStudSubCourseAsync(ProfStudSubCourseDto profStudSubCourseDto);
        Task<ProfStudSubCourseResponseDto> UpdateProfStudSubCourseAsync(ProfStudSubCourseResponseDto profStudSubCourseDto);
        Task<IEnumerable<ProfStudSubCourseResponseDto>> ListProfStudSubCoursesAsync();
        Task DeleteProfStudSubCourseByIdAsync(int id);
    }

}
