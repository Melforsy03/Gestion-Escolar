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
    public class ProfessorStudentSubjectService : IProfessorStudentSubjectService
    {
        private readonly IProfessorStudentSubjectRepository _professorStudentSubjectRepository;
        private readonly IMapper _mapper;

        public ProfessorStudentSubjectService(IProfessorStudentSubjectRepository professorStudentSubjectRepository, IMapper mapper)
        {
            _professorStudentSubjectRepository = professorStudentSubjectRepository;
            _mapper = mapper;
        }

        public async Task<ProfessorStudentSubjectDto> CreateProfessorStudentSubjectAsync(ProfessorStudentSubjectDto professorStudentSubjectDto)
        {
            var professorStudentSubject = _mapper.Map<Domain.Relations.ProfessorStudentSubject>(professorStudentSubjectDto);
            var savedProfessorStudentSubject = await _professorStudentSubjectRepository.CreateAsync(professorStudentSubject);
            return _mapper.Map<ProfessorStudentSubjectDto>(savedProfessorStudentSubject);
        }

        public async Task<IEnumerable<ProfessorStudentSubjectDto>> ListProfessorStudentSubjectAsync()
        {
            var professorStudentSubjects = await _professorStudentSubjectRepository.ListAsync();
            var list = professorStudentSubjects.ToList();
            List<ProfessorStudentSubjectDto> professorStudentSubjects_List = new();
            for (int i = 0; i < professorStudentSubjects.Count(); i++)
            {
                professorStudentSubjects_List.Add(_mapper.Map<ProfessorStudentSubjectDto>(list[i]));
            }

            return professorStudentSubjects_List;
        }

        public async Task<ProfessorStudentSubjectDto> UpdateProfessorStudentSubjectAsync(ProfessorStudentSubjectDto professorStudentSubjectDto)
        {
            var professorStudentSubject = await _professorStudentSubjectRepository.GetByIdAsync(professorStudentSubjectDto.IdProf);
            _mapper.Map(professorStudentSubjectDto, professorStudentSubject);
            await _professorStudentSubjectRepository.UpdateAsync(professorStudentSubject);
            return _mapper.Map<ProfessorStudentSubjectDto>(professorStudentSubject);
        }
    }
}
