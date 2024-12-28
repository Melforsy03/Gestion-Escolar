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
    public class ProfessorSubjectService : IProfessorSubjectService
    {
        private readonly IProfessorSubjectRepository _professorSubjectRepository;
        private readonly IMapper _mapper;

        public ProfessorSubjectService(IProfessorSubjectRepository professorSubjectRepository, IMapper mapper)
        {
            _professorSubjectRepository = professorSubjectRepository;
            _mapper = mapper;
        }

        public async Task<ProfessorSubjectDto> CreateProfessorSubjectAsync(ProfessorSubjectDto professorSubjectDto)
        {
            var professorSubject = _mapper.Map < Domain.Relations.ProfessorSubject>(professorSubjectDto);
            var savedAgency = await _professorSubjectRepository.CreateAsync(professorSubject);
            return _mapper.Map<ProfessorSubjectDto>(savedAgency);
        }

        public async Task DeleteProfessorSubjectByIdAsync(int professorSubjectId)
        {
            await _professorSubjectRepository.DeleteByIdAsync(professorSubjectId);
        }

        public async Task<IEnumerable<ProfessorSubjectDto>> ListProfessorSubjectAsync()
        {
            var professorSubject = await _professorSubjectRepository.ListAsync();
            var list = professorSubject.ToList();
            List<ProfessorSubjectDto> professorSubjectList = new();

            for (int i = 0; i < list.Count; i++)
            {
                professorSubjectList.Add(_mapper.Map<ProfessorSubjectDto>(list[i]));
            }

            return professorSubjectList;
        }

    }

}
