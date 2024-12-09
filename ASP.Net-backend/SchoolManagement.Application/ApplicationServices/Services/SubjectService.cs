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
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<SubjectDto> CreateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = _mapper.Map<Domain.Entities.Subject>(subjectDto);
            var savedAgency = await _subjectRepository.CreateAsync(subject);
            return _mapper.Map<SubjectDto>(savedAgency);
        }

        public async Task DeleteSubjectByIdAsync(int subjectDto)
        {
            await _subjectRepository.DeleteByIdAsync(subjectDto);
        }

        public async Task<IEnumerable<SubjectDto>> ListSubjectAsync()
        {
            var subjects = await _subjectRepository.ListAsync();
            var list = subjects.ToList();
            List<SubjectDto> Subject_List = new();
            for (int i = 0; i < subjects.Count(); i++)
            {
                Subject_List.Add(_mapper.Map<SubjectDto>(list[i]));
            }

            return Subject_List;
        }

        public async Task<SubjectDto> UpdateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = _subjectRepository.GetById(subjectDto.IdSub);
            _mapper.Map(subjectDto, subject);
            await _subjectRepository.UpdateAsync(subject);
            return _mapper.Map<SubjectDto>(subject);
        }
    }
}
