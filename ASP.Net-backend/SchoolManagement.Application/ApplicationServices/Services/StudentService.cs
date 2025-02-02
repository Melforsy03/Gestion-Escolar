using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Professor;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Student;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using SchoolManagement.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly Context _context;
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public StudentService(Context context, IStudentRepository studentRepository, IMapper mapper, Triggers trigger, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _context = context;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<StudentCreateResponseDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            (User, string) User = await _trigger.RegisterUser(studentDto.NameStud, "Student");
            student.UserId = User.Item1.Id;
            student.Course = _courseRepository.GetById(1);
            var savedStudent = await _studentRepository.CreateAsync(student);

            studentDto = _mapper.Map<StudentDto>(savedStudent);
            StudentCreateResponseDto answer = new StudentCreateResponseDto();
            answer.Id = student.IdStud;
            answer.IdC = student.IdC;
            answer.student = studentDto;
            answer.UserName = User.Item1.UserName;
            answer.Password = User.Item2;
            return answer;
        }
        public async Task<IEnumerable<StudentInfoBad>> GetBadStudents()
        {
            var students = _context.Students.ToList();
            List<StudentInfoBad> badStudents = new List<StudentInfoBad>();
            foreach (var student in students)
            {
                
                var studentSubjects = _context.StudentSubjects.Where(ss => ss.IdStud == student.IdStud).ToList();
                List<Subject> subjects = new List<Subject>();
                foreach(var ss in studentSubjects)
                {
                    var gradesTemp = _context.ProfessorStudentSubjects.Where(pss => pss.IdStudSub == ss.IdStudSub).Select(k => k.StudentGrades).ToList();
                    float average = _trigger.GetAverage(gradesTemp);
                    if(average < 50 && average >= 0)
                    {
                        subjects.Add(_context.Subjects.Where(s => s.IdSub == ss.IdSub).First());
                    }
                }

                if (subjects.Count() >= 2)
                {

                    List<Professor> checkedProfessors = new List<Professor>();

                    foreach (var s in subjects)
                    {

                        List<Professor> professors = new List<Professor>();
                        var professorSubjects = _context.ProfessorSubjects.Where(ps => ps.IdSub == s.IdSub).ToList();
                        foreach(var ps in professorSubjects)
                        {
                            professors.Add(_context.Professors.Where(p => p.IdProf == ps.IdProf).First());
                        }
                        foreach (var p in professors)
                        {
                            var grades = _context.ProfessorStudentSubjects.Where(pss => pss.IdProf == p.IdProf && pss.IdStudSub == _context.StudentSubjects.Where(ss => ss.IdSub == s.IdSub && ss.IdStud == student.IdStud).First().IdStudSub).Select(g => g.StudentGrades).ToList();
                            foreach(var g in grades)
                            {
                                if(g < 50)
                                {
                                    checkedProfessors.Add(p);
                                }
                            }
                        }

                    }

                    List<int> evals = new List<int>();
                    foreach (var p in checkedProfessors)
                    {
                        evals.AddRange(_context.ProfStudSubCourses.Where(pssc => pssc.IdProf == p.IdProf && pssc.IdCourse == student.IdC).Select(g => g.Evaluation).ToList());
                    }


                    StudentInfoBad badStudent = new StudentInfoBad();
                    badStudent.StudentId = student.IdStud;
                    badStudent.StudentName = student.NameStud;
                    badStudent.Curso = _context.Courses.Where(c => c.IdC == student.IdC).First().CourseName;
                    badStudent.ProfessorsAvarageEvaluation = _trigger.GetAverage(evals);

                    badStudents.Add(badStudent);
                }
            }
            return badStudents;
        }
        public async Task<StudentResponseDto> DeleteStudentByIdAsync(int studentId)
        {
            var student = _studentRepository.GetById(studentId);
            if (student.IsDeleted)
            {
                return null;
            }
            student.IsDeleted = true;
            await _studentRepository.UpdateAsync(student);
            StudentResponseDto answer = new StudentResponseDto();
            answer.Id = student.IdStud;
            answer.Idc = student.IdC;
            answer.student = _mapper.Map<StudentDto>(student);
            return answer;
        }

        public async Task<IEnumerable<StudentResponseDto>> ListStudentAsync()
        {
            var students = await _studentRepository.ListAsync();
            var list = students.ToList();
            List<StudentResponseDto> Students_List = new();

            for (int i = 0; i < students.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    StudentResponseDto answer = new StudentResponseDto();
                    answer.Id = list[i].IdStud;
                    answer.Idc = list[i].IdC;
                    answer.student = _mapper.Map<StudentDto>(list[i]);
                    Students_List.Add(answer);
                }
            }

            return Students_List;
        }

        public async Task<StudentResponseDto> UpdateStudentAsync(StudentResponseDto studentInfo)
        {
            var student = _studentRepository.GetById(studentInfo.Id);
            if (student.IsDeleted)
            {
                return null;
            }
            _mapper.Map(studentInfo.student, student);
            var course = _courseRepository.GetById(studentInfo.Idc);
            if (course == null) return null;
            student.Course = course;
            await _studentRepository.UpdateAsync(student);
            StudentResponseDto answer = new StudentResponseDto();
            answer.Id = student.IdStud;
            answer.Idc = student.IdC;
            answer.student = _mapper.Map<StudentDto>(student);
            return answer;
        }

    }
}
