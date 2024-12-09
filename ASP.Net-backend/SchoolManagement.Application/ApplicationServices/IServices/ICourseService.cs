using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CourseDto courseDto);
        Task<CourseDto> UpdateCourseAsync(CourseDto courseDto);
        Task<IEnumerable<CourseDto>> ListCoursesAsync();
        Task DeleteCourseByIdAsync(int courseDto);
    }
}
