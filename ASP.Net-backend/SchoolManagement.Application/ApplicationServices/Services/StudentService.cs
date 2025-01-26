using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto.Professor;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ResponseDto.Student;
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
        private readonly Triggers _trigger;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper, Triggers trigger)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _trigger = trigger;
        }

        public async Task<StudentCreateResponseDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            (User, string) User = await _trigger.RegisterUser(studentDto.NameStud, "Student");
            student.UserId = User.Item1.Id;
            var savedStudent = await _studentRepository.CreateAsync(student);

            studentDto = _mapper.Map<StudentDto>(savedStudent);
            StudentCreateResponseDto answer = new StudentCreateResponseDto();
            answer.Id = student.IdStud;
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
            await _studentRepository.UpdateAsync(student);
            StudentResponseDto answer = new StudentResponseDto();
            answer.Id = student.IdStud;
            answer.student = _mapper.Map<StudentDto>(student);
            return answer;
        }

    }
}
