using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ProfessorStudentSubject;
using SchoolManagement.Domain.Relations;
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
        private readonly IProfessorRepository _professorRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IMapper _mapper;

        public ProfessorStudentSubjectService(IProfessorStudentSubjectRepository professorStudentSubjectRepository, IProfessorRepository professorRepository, IStudentSubjectRepository studentSubjectRepository,IMapper mapper)
        {
            _professorStudentSubjectRepository = professorStudentSubjectRepository;
            _professorRepository = professorRepository;
            _studentSubjectRepository = studentSubjectRepository;
            _mapper = mapper;
        }

        public async Task<ProfessorStudentSubjectResponseDto> CreateProfessorStudentSubjectAsync(ProfessorStudentSubjectDto professorStudentSubjectDto)
        {
            var professorStudentSubject = _mapper.Map<ProfessorStudentSubject>(professorStudentSubjectDto);
            professorStudentSubject.Professor = _professorRepository.GetById(professorStudentSubjectDto.IdProf);
            professorStudentSubject.StudentSubject = _studentSubjectRepository.GetById(professorStudentSubjectDto.IdStudSub);            
            var savedProfessorStudentSubject = await _professorStudentSubjectRepository.CreateAsync(professorStudentSubject);
            return _mapper.Map<ProfessorStudentSubjectResponseDto>(savedProfessorStudentSubject);
        }

        public async Task<IEnumerable<ProfessorStudentSubjectResponseDto>> ListProfessorStudentSubjectAsync()
        {
            var professorStudentSubjects = await _professorStudentSubjectRepository.ListAsync();
            var list = professorStudentSubjects.ToList();
            List<ProfessorStudentSubjectResponseDto> professorStudentSubjects_List = new();
            for (int i = 0; i < professorStudentSubjects.Count(); i++)
            {
                professorStudentSubjects_List.Add(_mapper.Map<ProfessorStudentSubjectResponseDto>(list[i]));
            }

            return professorStudentSubjects_List;
        }

        public async Task<ProfessorStudentSubjectResponseDto> UpdateProfessorStudentSubjectAsync(ProfessorStudentSubjectResponseDto professorStudentSubjectDto)
        {
            var professorStudentSubject = await _professorStudentSubjectRepository.GetByIdAsync(professorStudentSubjectDto.IdProfStudSub);
            _mapper.Map(professorStudentSubjectDto, professorStudentSubject);
            await _professorStudentSubjectRepository.UpdateAsync(professorStudentSubject);
            return _mapper.Map<ProfessorStudentSubjectResponseDto>(professorStudentSubject);
        }
    }
}
