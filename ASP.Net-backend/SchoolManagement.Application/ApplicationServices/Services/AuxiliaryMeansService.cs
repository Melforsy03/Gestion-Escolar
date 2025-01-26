using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.AuxiliaryMeans;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using SchoolManagement.Infrastructure.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class AuxiliaryMeansService : IAuxiliaryMeansService
    {
        private readonly IAuxiliaryMeansRepository _auxiliaryMeansRepository;
        private readonly IMapper _mapper;

        public AuxiliaryMeansService(IAuxiliaryMeansRepository auxiliaryMeansRepository, IMapper mapper)
        {
            _auxiliaryMeansRepository = auxiliaryMeansRepository;
            _mapper = mapper;
        }

        public async Task<AuxiliaryMeansResponseDto> CreateAuxiliaryMeansAsync(AuxiliaryMeansDto auxiliaryMeansDto)
        {
            var auxiliaryMeans = _mapper.Map<Domain.Entities.AuxiliaryMeans>(auxiliaryMeansDto);
            var savedAuxiliaryMeans = await _auxiliaryMeansRepository.CreateAsync(auxiliaryMeans);
            return _mapper.Map<AuxiliaryMeansResponseDto>(savedAuxiliaryMeans);
        }

        public async Task<AuxiliaryMeansResponseDto> DeleteAuxiliaryMeansByIdAsync(int auxiliaryMeansId)
        {
            var auxiliaryMean = _auxiliaryMeansRepository.GetById(auxiliaryMeansId);
            if (auxiliaryMean.isDeleted)
            {
                return null;
            }
            auxiliaryMean.isDeleted = true;
            await _auxiliaryMeansRepository.UpdateAsync(auxiliaryMean);
            return _mapper.Map<AuxiliaryMeansResponseDto>(auxiliaryMean);
        }

        public async Task<IEnumerable<AuxiliaryMeansResponseDto>> ListAuxiliaryMeansAsync()
        {
            var auxiliaryMeansList = await _auxiliaryMeansRepository.ListAsync();
            var list = auxiliaryMeansList.ToList();
            List<AuxiliaryMeansResponseDto> auxiliaryMeansDtos = new();
            for (int i = 0; i < auxiliaryMeansList.Count(); i++)
            {
                if (!list[i].isDeleted)
                {
                    auxiliaryMeansDtos.Add(_mapper.Map<AuxiliaryMeansResponseDto>(list[i]));
                }
            }

            return auxiliaryMeansDtos;
        }

        public async Task<AuxiliaryMeansResponseDto> UpdateAuxiliaryMeansAsync(AuxiliaryMeansResponseDto auxiliaryMeansDto)
        {
            var auxiliaryMeans = _auxiliaryMeansRepository.GetById(auxiliaryMeansDto.IdMean);
            if (auxiliaryMeans.isDeleted) return null;
            _mapper.Map(auxiliaryMeansDto, auxiliaryMeans);
            await _auxiliaryMeansRepository.UpdateAsync(auxiliaryMeans);
            return _mapper.Map<AuxiliaryMeansResponseDto>(auxiliaryMeans);
        }
    }
}
