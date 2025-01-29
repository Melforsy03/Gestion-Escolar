using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfStudSubCourse;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure;
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
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ProfStudSubCourseService(Context context, IProfStudSubCourseRepository profStudSubCourseRepository, ISubjectRepository subjectRepository, IStudentRepository studentRepository, IProfessorRepository professorRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _profStudSubCourseRepository = profStudSubCourseRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _professorRepository = professorRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProfStudSubCourseResponseDto> CreateProfStudSubCourseAsync(ProfStudSubCourseDto profStudSubCourseDto)
        {
            var profStudSubCourse = _mapper.Map<ProfStudSubCourse>(profStudSubCourseDto);
            profStudSubCourse.Course = await _courseRepository.GetByIdAsync(profStudSubCourseDto.IdCourse);
            profStudSubCourse.Professor = await _professorRepository.GetByIdAsync(profStudSubCourseDto.IdProf);
            profStudSubCourse.Student = await _studentRepository.GetByIdAsync(profStudSubCourseDto.IdStud);
            profStudSubCourse.Subject = await _subjectRepository.GetByIdAsync(profStudSubCourseDto.IdSub);

            var savedProfStudSubCourse = await _profStudSubCourseRepository.CreateAsync(profStudSubCourse);
            var answer = _mapper.Map<ProfStudSubCourseResponseDto>(savedProfStudSubCourse);
            answer.StudentName = profStudSubCourse.Student.NameStud;
            answer.ProfessorName = profStudSubCourse.Professor.NameProf;
            answer.CourseName = profStudSubCourse.Course.CourseName;
            answer.SubjectName = profStudSubCourse.Subject.NameSub;

            return answer;
        }

        public async Task DeleteProfStudSubCourseByIdAsync(int id)
        {
            await _profStudSubCourseRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<ProfStudSubCourseResponseDto>> ListProfStudSubCoursesAsync()
        {
            var profStudSubCourses = await _profStudSubCourseRepository.ListAsync();
            var list = profStudSubCourses.ToList();
            List<ProfStudSubCourseResponseDto> profStudSubCoursesList = new();

            for (int i = 0; i < list.Count; i++)
            {
                var temp = _mapper.Map<ProfStudSubCourseResponseDto>(list[i]);
                temp.StudentName = _context.Students.Find(temp.IdStud).NameStud;
                temp.CourseName = _context.Courses.Find(temp.IdCourse).CourseName;
                temp.SubjectName = _context.Subjects.Find(temp.IdSub).NameSub;
                temp.ProfessorName = _context.Professors.Find(temp.IdProf).NameProf;

                profStudSubCoursesList.Add(temp);
            }

            return profStudSubCoursesList;
        }
        public async Task<IEnumerable<ProfStudSubCourseResponseDto>> ListProfStudSubCoursesByProfAsync(ProfStudSubCourseConsultDto profStudSubCourseConsultDto)
        {
            var list = _context.ProfStudSubCourses.Where(pssc => pssc.IdProf == _context.Professors.Where(p => p.UserId == _context.Users.Where(u => u.UserName == profStudSubCourseConsultDto.UserName).First().Id).First().IdProf).ToList();
           
            List<ProfStudSubCourseResponseDto> profStudSubCoursesList = new();

            for (int i = 0; i < list.Count; i++)
            {
                var temp = _mapper.Map<ProfStudSubCourseResponseDto>(list[i]);
                temp.StudentName = _context.Students.Find(temp.IdStud).NameStud;
                temp.CourseName = _context.Courses.Find(temp.IdCourse).CourseName;
                temp.SubjectName = _context.Subjects.Find(temp.IdSub).NameSub;
                temp.ProfessorName = _context.Professors.Find(temp.IdProf).NameProf;

                profStudSubCoursesList.Add(temp);
            }

            return profStudSubCoursesList;
        }
        public async Task<IEnumerable<ProfStudSubCourseConsultResponseDto>> GetProfessors (string UserName)
        {
            var user = _context.Users.Where(u => u.UserName == UserName).First();
            var student = _context.Students.Where(s => s.UserId == user.Id).First();
            var studentSubject = _context.StudentSubjects.Where(ss => ss.IdStud == student.IdStud).ToList();
            var course = _context.Courses.Where(c => c.IdC == student.IdC).First();
            List<ProfStudSubCourseConsultResponseDto> list = new List<ProfStudSubCourseConsultResponseDto>();
            for(int i = 0; i < studentSubject.Count; i++)
            {
                var professorSubjects =  _context.ProfessorSubjects.Where(ps => ps.IdSub == studentSubject[i].IdSub).ToList();

                for (int j = 0; j < professorSubjects.Count(); j++)
                {
                    ProfStudSubCourseConsultResponseDto temp = new ProfStudSubCourseConsultResponseDto();
                   
                    temp.IdSub = professorSubjects[j].IdSub;
                    temp.IdCourse = course.IdC;
                    temp.IdStud = student.IdStud;
                    temp.IdProf = professorSubjects[j].IdProf;
                    temp.profName = _context.Professors.Where(p => p.IdProf == professorSubjects[j].IdProf).First().NameProf;
                    temp.subjectName = _context.Subjects.Where(s => s.IdSub == professorSubjects[j].IdSub).First().NameSub;
                    temp.CourseName = course.CourseName;
                    if(!list.Contains(temp)) list.Add(temp);

                }
            }

            return list;
        }
        public async Task<ProfStudSubCourseResponseDto> UpdateProfStudSubCourseAsync(ProfStudSubCourseResponseDto profStudSubCourseDto)
        {
            var profStudSubCourse = await _profStudSubCourseRepository.GetByIdAsync(profStudSubCourseDto.IdProfStudSubCourse);
            _mapper.Map(profStudSubCourseDto, profStudSubCourse);
            await _profStudSubCourseRepository.UpdateAsync(profStudSubCourse);
            return _mapper.Map<ProfStudSubCourseResponseDto>(profStudSubCourse);
        }

        public async Task<ProfStudSubCouseConsultResponseDto> GetEvaluationByCourse(ProfStudSubCourseConsultDto profStudSubCourseConsultDto)
        {

            throw new NotImplementedException();
        }
    }

}
