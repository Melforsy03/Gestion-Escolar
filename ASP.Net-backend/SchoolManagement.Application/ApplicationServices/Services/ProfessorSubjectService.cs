using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Relations;
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

            var professorList = await _professorRepository.ListAsync();
            var list0 = professorList.ToList();
            Professor ProfessorFinded = new Professor();
            for(int i = 0; i < list0.Count; i++)
            {
                if (list0[i].IdProf == professorSubjectDto.IdProf)
                {
                    ProfessorFinded = list0[i];
                }
            }

            var subjectList = await _subjectRepository.ListAsync();
            var list1 = subjectList.ToList();
            Subject SubjectFinded = new Subject();
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].IdSub == professorSubjectDto.IdSub)
                {
                    SubjectFinded = list1[i];
                }
            }

            var professorSubject = new ProfessorSubject
            {
                IdProf = professorSubjectDto.IdProf,
                IdSub = professorSubjectDto.IdSub,
                Professor = ProfessorFinded,
                Subject = SubjectFinded
                
            };

            var savedAgency = await _professorSubjectRepository.CreateAsync(professorSubject);

            return new ProfessorSubjectDto
            {
                IdProf = savedAgency.IdProf,
                IdSub = savedAgency.IdSub
            };
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
