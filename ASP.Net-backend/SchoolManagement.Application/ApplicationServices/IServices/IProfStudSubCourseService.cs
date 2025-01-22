using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.IServices
{
    public interface IProfStudSubCourseService
    {
        Task<ProfStudSubCourseDto> CreateProfStudSubCourseAsync(ProfStudSubCourseDto profStudSubCourseDto);
        Task<ProfStudSubCourseDto> UpdateProfStudSubCourseAsync(ProfStudSubCourseDto profStudSubCourseDto);
        Task<IEnumerable<ProfStudSubCourseDto>> ListProfStudSubCoursesAsync();
        Task DeleteProfStudSubCourseByIdAsync(int id);
    }

}
