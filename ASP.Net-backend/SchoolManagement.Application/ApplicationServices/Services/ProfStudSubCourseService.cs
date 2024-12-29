using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ProfStudSubCourseService : IProfStudSubCourseService
    {
        private readonly IProfStudSubCourseRepository _profStudSubCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public ProfStudSubCourseService(IProfStudSubCourseRepository profStudSubCourseRepository, ISubjectRepository subjectRepository, IStudentRepository studentRepository, IProfessorRepository professorRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _profStudSubCourseRepository = profStudSubCourseRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _professorRepository = professorRepository;
            _mapper = mapper;
        }

        public async Task<ProfStudSubCourseDto> CreateProfStudSubCourseAsync(ProfStudSubCourseDto profStudSubCourseDto)
        {
            var profStudSubCourse = _mapper.Map<ProfStudSubCourse>(profStudSubCourseDto);
            profStudSubCourse.Course = await _courseRepository.GetByIdAsync(profStudSubCourseDto.IdCourse);
            profStudSubCourse.Professor = await _professorRepository.GetByIdAsync(profStudSubCourseDto.IdProf);
            profStudSubCourse.Student = await _studentRepository.GetByIdAsync(profStudSubCourseDto.IdStud);
            profStudSubCourse.Subject = await _subjectRepository.GetByIdAsync(profStudSubCourseDto.IdSub);

            var savedProfStudSubCourse = await _profStudSubCourseRepository.CreateAsync(profStudSubCourse);
            return _mapper.Map<ProfStudSubCourseDto>(savedProfStudSubCourse);
        }

        public async Task DeleteProfStudSubCourseByIdAsync(int id)
        {
            await _profStudSubCourseRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<ProfStudSubCourseDto>> ListProfStudSubCoursesAsync()
        {
            var profStudSubCourses = await _profStudSubCourseRepository.ListAsync();
            var list = profStudSubCourses.ToList();
            List<ProfStudSubCourseDto> profStudSubCoursesList = new();

            for (int i = 0; i < list.Count; i++)
            {
                profStudSubCoursesList.Add(_mapper.Map<ProfStudSubCourseDto>(list[i]));
            }

            return profStudSubCoursesList;
        }

        public async Task<ProfStudSubCourseDto> UpdateProfStudSubCourseAsync(ProfStudSubCourseDto profStudSubCourseDto)
        {
            var profStudSubCourse = await _profStudSubCourseRepository.GetByIdAsync(profStudSubCourseDto.IdProfStudSubCourse);
            _mapper.Map(profStudSubCourseDto, profStudSubCourse);
            await _profStudSubCourseRepository.UpdateAsync(profStudSubCourse);
            return _mapper.Map<ProfStudSubCourseDto>(profStudSubCourse);
        }
    }

}
