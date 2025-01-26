using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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

        public async Task<(int Id, StudentDto student, string UserName, string Password)> CreateStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            (User, string) User = await _trigger.RegisterUser(studentDto.NameStud, "Student");
            student.UserId = User.Item1.Id;
            var savedStudent = await _studentRepository.CreateAsync(student);

            studentDto = _mapper.Map<StudentDto>(savedStudent);
            return (student.IdStud, studentDto, User.Item1.UserName, User.Item2);
        }

        public async Task<(int Id, StudentDto student)> DeleteStudentByIdAsync(int studentId)
        {
            var student = _studentRepository.GetById(studentId);
            if (student.IsDeleted)
            {
                return (0,null);
            }
            student.IsDeleted = true;
            await _studentRepository.UpdateAsync(student);
            return (student.IdStud, _mapper.Map<StudentDto>(student));
        }

        public async Task<IEnumerable<(int Id, StudentDto student)>> ListStudentAsync()
        {
            var students = await _studentRepository.ListAsync();
            var list = students.ToList();
            List<(int Id, StudentDto student)> Students_List = new();

            for (int i = 0; i < students.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    Students_List.Add((list[i].IdStud, _mapper.Map<StudentDto>(list[i])));
                }
            }

            return Students_List;
        }

        public async Task<(int Id, StudentDto student)> UpdateStudentAsync((int Id, StudentDto studentDto) studentInfo)
        {
            var student = _studentRepository.GetById(studentInfo.Id);
            if (!student.IsDeleted)
            {
                return (0,null);
            }
            _mapper.Map(studentInfo.studentDto, student);
            await _studentRepository.UpdateAsync(student);
            return (student.IdStud, _mapper.Map<StudentDto>(student));
        }

    }
}
