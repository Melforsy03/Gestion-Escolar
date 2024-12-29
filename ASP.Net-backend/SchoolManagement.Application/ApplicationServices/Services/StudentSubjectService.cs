using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class StudentSubjectService : IStudentSubjectService
    {
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public StudentSubjectService(IStudentSubjectRepository studentSubjectRepository,  IMapper mapper, IStudentRepository studentRepository, ISubjectRepository subjectRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<StudentSubjectDto> CreateStudentSubjectAsync(StudentSubjectDto studentSubjectDto)
        {
            var studentSubject = _mapper.Map<StudentSubject>(studentSubjectDto);
            studentSubject.Subject = _subjectRepository.GetById(studentSubjectDto.IdSub);
            studentSubject.Student = _studentRepository.GetById(studentSubjectDto.IdStud);

            var savedStudentSubject = await _studentSubjectRepository.CreateAsync(studentSubject);

            return _mapper.Map<StudentSubjectDto>(savedStudentSubject);

            
        }


        public async Task<IEnumerable<StudentSubjectDto>> ListStudentSubjectAsync()
        {
            var studentsSubject = await _studentSubjectRepository.ListAsync();
            var list = studentsSubject.ToList();
            List<StudentSubjectDto> studentsSubject_List = new();
            for (int i = 0; i < studentsSubject.Count(); i++)
            {
                studentsSubject_List.Add(_mapper.Map<StudentSubjectDto>(list[i]));
            }

            return studentsSubject_List;
        }

        public async Task<StudentSubjectDto> UpdateStudentSubjectAsync(StudentSubjectDto studentSubjectDto)
        {
            var studentSubject = _studentSubjectRepository.GetById(studentSubjectDto.IdStudSub);
            _mapper.Map(studentSubjectDto, studentSubject);
            await _studentSubjectRepository.UpdateAsync(studentSubject);
            return _mapper.Map<StudentSubjectDto>(studentSubject);
        }
    }
}
