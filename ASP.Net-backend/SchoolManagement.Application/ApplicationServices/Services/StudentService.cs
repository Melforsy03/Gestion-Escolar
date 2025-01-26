using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Professor;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Student;
using SchoolManagement.Application.Common;
using SchoolManagement.Domain.Entities;
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
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper, Triggers trigger, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
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
