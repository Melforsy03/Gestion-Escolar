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
    public class ProfessorSubjectService : IProfessorSubjectService
    {
        private readonly IProfessorSubjectRepository _professorSubjectRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public ProfessorSubjectService(IProfessorSubjectRepository professorSubjectRepository, ISubjectRepository subjectRepository, IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorSubjectRepository = professorSubjectRepository;
            _professorRepository = professorRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<ProfessorSubjectDto> CreateProfessorSubjectAsync(ProfessorSubjectDto professorSubjectDto)
        {

            var professorSubject = _mapper.Map<ProfessorSubject>(professorSubjectDto);
            professorSubject.Professor = _professorRepository.GetById(professorSubjectDto.IdProf);
            professorSubject.Subject = _subjectRepository.GetById(professorSubjectDto.IdSub);
            var savedProfessorSubject = await _professorSubjectRepository.CreateAsync(professorSubject);
            return _mapper.Map<ProfessorSubjectDto>(savedProfessorSubject);
        }

        public async Task DeleteProfessorSubjectByIdAsync(int professorSubjectId)
        {
            await _professorSubjectRepository.DeleteByIdAsync(professorSubjectId);
        }

        public async Task<IEnumerable<ProfessorSubjectDto>> ListProfessorSubjectAsync()
        {
            var professorSubjects = await _professorSubjectRepository.ListAsync();
            var list = professorSubjects.ToList();
            List<ProfessorSubjectDto> professorSubjects_List = new();
            for (int i = 0; i < professorSubjects.Count(); i++)
            {
                professorSubjects_List.Add(_mapper.Map<ProfessorSubjectDto>(list[i]));
            }

            return professorSubjects_List;
        }

    }

}
