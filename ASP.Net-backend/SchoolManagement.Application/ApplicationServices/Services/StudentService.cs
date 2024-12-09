using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
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
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Domain.Entities.Student>(studentDto);
            var savedStudent = await _studentRepository.CreateAsync(student);
            return _mapper.Map<StudentDto>(savedStudent);
        }

        public async Task DeleteStudentByIdAsync(int studentDto)
        {
            await _studentRepository.DeleteByIdAsync(studentDto);
        }

        public async Task<IEnumerable<StudentDto>> ListStudentAsync()
        {
            var students = await _studentRepository.ListAsync();
            var list = students.ToList();
            List<StudentDto> students_List = new();
            for (int i = 0; i < students.Count(); i++)
            {
                students_List.Add(_mapper.Map<StudentDto>(list[i]));
            }

            return students_List;
        }

        public async Task<StudentDto> UpdateStudentAsync(StudentDto studentDto)
        {
            var student = _studentRepository.GetById(studentDto.IdStud);
            _mapper.Map(studentDto, student);
            await _studentRepository.UpdateAsync(student);
            return _mapper.Map<StudentDto>(student);
        }
    }
}
