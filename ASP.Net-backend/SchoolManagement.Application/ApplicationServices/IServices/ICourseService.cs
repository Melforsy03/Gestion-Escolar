using SchoolManagement.Application.ApplicationServices.Maps_Dto.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ICourseService
    {
        Task<CourseResponseDto> CreateCourseAsync(CourseDto courseDto);
        Task<CourseResponseDto> UpdateCourseAsync(CourseResponseDto courseDto);
        Task<IEnumerable<CourseResponseDto>> ListCoursesAsync();
        Task<CourseResponseDto> DeleteCourseByIdAsync(int courseDto);
    }
}
