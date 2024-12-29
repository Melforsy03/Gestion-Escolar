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
    public class SubjectAuxMeanService : ISubjectAuxMeanService
    {
        private readonly ISubjectAuxMeanRepository _subjectAuxMeanRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAuxiliaryMeansRepository _auxiliaryMeansRepository;
        private readonly IMapper _mapper;

        public SubjectAuxMeanService(ISubjectAuxMeanRepository subjectAuxMeanRepository, IMapper mapper, ISubjectRepository subjectRepository, IAuxiliaryMeansRepository auxiliaryMeansRepository)
        {
            _subjectAuxMeanRepository = subjectAuxMeanRepository;
            _mapper = mapper;
            _auxiliaryMeansRepository = auxiliaryMeansRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<SubjectAuxMeanDto> CreateSubjectAuxMeanAsync(SubjectAuxMeanDto subjectAuxMeanDto)
        {
            var subjectAuxMean = _mapper.Map<Domain.Relations.SubjectAuxMean>(subjectAuxMeanDto);
            subjectAuxMean.Subject = await _subjectRepository.GetByIdAsync(subjectAuxMean.IdSub);
            subjectAuxMean.AuxMean = await _auxiliaryMeansRepository.GetByIdAsync(subjectAuxMean.IdAuxMean);
            var savedSubjectAuxMean = await _subjectAuxMeanRepository.CreateAsync(subjectAuxMean);
            return _mapper.Map<SubjectAuxMeanDto>(savedSubjectAuxMean);
        }

        public async Task DeleteSubjectAuxMeanByIdAsync(int id)
        {
            await _subjectAuxMeanRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<SubjectAuxMeanDto>> ListSubjectAuxMeansAsync()
        {
            var subjectAuxMeans = await _subjectAuxMeanRepository.ListAsync();
            var list = subjectAuxMeans.ToList();
            List<SubjectAuxMeanDto> subjectAuxMeansList = new();

            for (int i = 0; i < list.Count; i++)
            {
                subjectAuxMeansList.Add(_mapper.Map<SubjectAuxMeanDto>(list[i]));
            }

            return subjectAuxMeansList;
        }

        public async Task<SubjectAuxMeanDto> UpdateSubjectAuxMeanAsync(SubjectAuxMeanDto subjectAuxMeanDto)
        {
            var subjectAuxMean = await _subjectAuxMeanRepository.GetByIdAsync(subjectAuxMeanDto.IdSub);
            _mapper.Map(subjectAuxMeanDto, subjectAuxMean);
            await _subjectAuxMeanRepository.UpdateAsync(subjectAuxMean);
            return _mapper.Map<SubjectAuxMeanDto>(subjectAuxMean);
        }
    }

}
