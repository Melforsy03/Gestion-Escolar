using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Course;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<CourseResponseDto> CreateCourseAsync(CourseDto courseDto)
        {
            var course = _mapper.Map<Domain.Entities.Course>(courseDto);
            var savedCourse = await _courseRepository.CreateAsync(course);
            return _mapper.Map<CourseResponseDto>(savedCourse);
        }

        public async Task<CourseResponseDto> DeleteCourseByIdAsync(int courseId)
        {
            var course = _courseRepository.GetById(courseId);
            if (course.IsDeleted) return null;
            course.IsDeleted = true;
            await _courseRepository.UpdateAsync(course);
            return _mapper.Map<CourseResponseDto>(course);
        }

        public async Task<IEnumerable<CourseResponseDto>> ListCoursesAsync()
        {
            var courses = await _courseRepository.ListAsync();
            var list = courses.ToList();
            List<CourseResponseDto> coursesList = new();
            for (int i = 0; i < courses.Count(); i++)
            {   
                if(!list[i].IsDeleted) coursesList.Add(_mapper.Map<CourseResponseDto>(list[i]));
            }

            return coursesList;
        }

        public async Task<CourseResponseDto> UpdateCourseAsync(CourseResponseDto courseDto)
        {
            var course =  _courseRepository.GetById(courseDto.IdC);
            if (course.IsDeleted) return null;
            _mapper.Map(courseDto, course);
            await _courseRepository.UpdateAsync(course);
            return _mapper.Map<CourseResponseDto>(course);
        }
    }
}
