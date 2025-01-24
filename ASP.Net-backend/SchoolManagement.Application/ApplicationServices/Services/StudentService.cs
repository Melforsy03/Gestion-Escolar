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

        public async Task<StudentDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            User User = await _trigger.RegisterUser(studentDto.NameStud, "Student");
            student.UserId = User.Id;
            var savedStudent = await _studentRepository.CreateAsync(student);

            studentDto = _mapper.Map<StudentDto>(savedStudent);
            studentDto.UserName = User.UserName;
            studentDto.PasswordHash = User.PasswordHash;
            return studentDto;
        }

        public async Task<StudentDto> DeleteStudentByIdAsync(int studentId)
        {
            var student = _studentRepository.GetById(studentId);
            if (student.IsDeleted)
            {
                return null;
            }
            student.IsDeleted = true;
            await _studentRepository.UpdateAsync(student);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<IEnumerable<StudentDto>> ListStudentAsync()
        {
            var students = await _studentRepository.ListAsync();
            var list = students.ToList();
            List<StudentDto> Students_List = new();

            for (int i = 0; i < students.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    Students_List.Add(_mapper.Map<StudentDto>(list[i]));
                }
            }

            return Students_List;
        }

        public async Task<StudentDto> UpdateStudentAsync(StudentDto studentDto)
        {
            var student = _studentRepository.GetById(studentDto.IdStud);
            if (!student.IsDeleted)
            {
                return null;
            }
            _mapper.Map(studentDto, student);
            await _studentRepository.UpdateAsync(student);
            return _mapper.Map<StudentDto>(student);
        }

    }
}
