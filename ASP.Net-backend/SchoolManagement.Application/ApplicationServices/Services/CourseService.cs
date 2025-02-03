using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Course;
using SchoolManagement.Application.Common;
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
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public CourseService(Triggers trigger, ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _trigger = trigger;
            _mapper = mapper;
        }

        public async Task<CourseResponseDto> CreateCourseAsync(CourseDto courseDto)
        {
            // Mapea el DTO a la entidad de dominio Course
            var course = _mapper.Map<Domain.Entities.Course>(courseDto);

            // Crea el curso en la base de datos y guarda el resultado
            var savedCourse = await _courseRepository.CreateAsync(course);

            // Verifica si hay profesores problemáticos asociados al curso
            _trigger.CheckBadProfessors(savedCourse.IdC);

            // Verifica si hay medios problemáticos asociados al curso
            _trigger.CheckBadMeans();

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<CourseResponseDto>(savedCourse);
        }

        public async Task<CourseResponseDto> DeleteCourseByIdAsync(int courseId)
        {
            // Obtiene el curso por su ID
            var course = _courseRepository.GetById(courseId);

            // Verifica si el curso ya está marcado como eliminado
            if (course.IsDeleted) return null;

            // Marca el curso como eliminado
            course.IsDeleted = true;

            // Actualiza el estado en la base de datos
            await _courseRepository.UpdateAsync(course);

            // Mapea y retorna el curso eliminado como DTO
            return _mapper.Map<CourseResponseDto>(course);
        }

        public async Task<IEnumerable<CourseResponseDto>> ListCoursesAsync()
        {
            // Obtiene todos los cursos desde el repositorio
            var courses = await _courseRepository.ListAsync();

            var list = courses.ToList(); // Convierte a lista para su manipulación
            List<CourseResponseDto> coursesList = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada curso y agrega solo los no eliminados a la lista de resultados
            for (int i = 0; i < courses.Count(); i++)
            {
                if (!list[i].IsDeleted)
                    coursesList.Add(_mapper.Map<CourseResponseDto>(list[i])); // Mapea y agrega a la lista
            }

            // Retorna la lista de DTOs de cursos no eliminados
            return coursesList;
        }

        public async Task<CourseResponseDto> UpdateCourseAsync(CourseResponseDto courseDto)
        {
            // Obtiene el curso por su ID desde el DTO
            var course = _courseRepository.GetById(courseDto.IdC);

            // Verifica si el curso está marcado como eliminado
            if (course.IsDeleted) return null;

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(courseDto, course);

            // Actualiza la entidad en la base de datos
            await _courseRepository.UpdateAsync(course);

            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<CourseResponseDto>(course);
        }

    }
}
