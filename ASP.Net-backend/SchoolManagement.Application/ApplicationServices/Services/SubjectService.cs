using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Subject;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
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
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository,  IMapper mapper, IClassRoomRepository classRoomRepository)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _classRoomRepository = classRoomRepository;
        }

        public async Task<SubjectResponseDto> CreateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = _mapper.Map<Domain.Entities.Subject>(subjectDto);
            subject.classRoom = await _classRoomRepository.GetByIdAsync(subject.IdClassRoom);
            var savedAgency = await _subjectRepository.CreateAsync(subject);
            return _mapper.Map<SubjectResponseDto>(savedAgency);
        }

        public async Task<SubjectResponseDto> DeleteSubjectByIdAsync(int subjectId)
        {
            var subject = _subjectRepository.GetById(subjectId);
            if (subject.IsDeleted) return null;
            subject.IsDeleted = true;
            await _subjectRepository.UpdateAsync(subject);
            return _mapper.Map<SubjectResponseDto>(subject);
        }

        public async Task<IEnumerable<SubjectResponseDto>> ListSubjectAsync()
        {
            var subjects = await _subjectRepository.ListAsync();
            var list = subjects.ToList();
            List<SubjectResponseDto> Subject_List = new();
            for (int i = 0; i < subjects.Count(); i++)
            {
                if(!list[i].IsDeleted) Subject_List.Add(_mapper.Map<SubjectResponseDto>(list[i]));
            }

            return Subject_List;
        }

        public async Task<SubjectResponseDto> UpdateSubjectAsync(SubjectResponseDto subjectDto)
        {
            var subject = _subjectRepository.GetById(subjectDto.IdSub);
            if (subject.IsDeleted) return null;
            _mapper.Map(subjectDto, subject);
            await _subjectRepository.UpdateAsync(subject);
            return _mapper.Map<SubjectResponseDto>(subject);
        }
    }
}
