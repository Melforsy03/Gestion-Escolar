using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.SubjectAuxMean;
using SchoolManagement.Domain.Relations;
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

        public async Task<SubjectAuxMeanResponseDto> CreateSubjectAuxMeanAsync(SubjectAuxMeanDto subjectAuxMeanDto)
        {
            var subjectAuxMean = _mapper.Map<Domain.Relations.SubjectAuxMean>(subjectAuxMeanDto);
            subjectAuxMean.Subject = await _subjectRepository.GetByIdAsync(subjectAuxMean.IdSub);
            subjectAuxMean.AuxMean = await _auxiliaryMeansRepository.GetByIdAsync(subjectAuxMean.IdAuxMean);
            var savedSubjectAuxMean = await _subjectAuxMeanRepository.CreateAsync(subjectAuxMean);
            return new SubjectAuxMeanResponseDto { IdAuxMean = savedSubjectAuxMean.IdAuxMean, IdSub = savedSubjectAuxMean.IdSub, IdSubAuxMean = savedSubjectAuxMean.IdSubAuxMean };
        }

        public async Task<SubjectAuxMeanResponseDto> DeleteSubjectAuxMeanByIdAsync(int id)
        {
            var subjectAuxMean = _subjectAuxMeanRepository.GetById(id);
            if (subjectAuxMean == null) return null;
            SubjectAuxMeanResponseDto subjectAuxMeanResponse = new SubjectAuxMeanResponseDto { IdAuxMean = subjectAuxMean.IdAuxMean, IdSub = subjectAuxMean.IdSub, IdSubAuxMean = subjectAuxMean.IdSubAuxMean };
            await _subjectAuxMeanRepository.DeleteByIdAsync(id);
            return subjectAuxMeanResponse;
        }

        public async Task<IEnumerable<SubjectAuxMeanResponseDto>> ListSubjectAuxMeansAsync()
        {
            var subjectAuxMeans = await _subjectAuxMeanRepository.ListAsync();
            var list = subjectAuxMeans.ToList();
            List<SubjectAuxMeanResponseDto> subjectAuxMeansList = new();

            for (int i = 0; i < list.Count; i++)
            {
                subjectAuxMeansList.Add(new SubjectAuxMeanResponseDto { IdAuxMean =  list[i].IdAuxMean, IdSub = list[i].IdSub, IdSubAuxMean = list[i].IdSubAuxMean });
            }

            return subjectAuxMeansList;
        }

        public async Task<SubjectAuxMeanResponseDto> UpdateSubjectAuxMeanAsync(SubjectAuxMeanResponseDto subjectAuxMeanResponseDto)
        {
            var subjectAuxMean = await _subjectAuxMeanRepository.GetByIdAsync(subjectAuxMeanResponseDto.IdSubAuxMean);
            _mapper.Map(subjectAuxMeanResponseDto, subjectAuxMean);
            await _subjectAuxMeanRepository.UpdateAsync(subjectAuxMean);
            return new SubjectAuxMeanResponseDto { IdSubAuxMean = subjectAuxMean.IdSubAuxMean, IdAuxMean = subjectAuxMean.IdAuxMean, IdSub = subjectAuxMean.IdSub };
        }
    }

}
