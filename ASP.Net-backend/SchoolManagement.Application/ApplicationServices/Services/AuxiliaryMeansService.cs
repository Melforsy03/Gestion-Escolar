using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto;
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

        public async Task<AuxiliaryMeansDto> CreateAuxiliaryMeansAsync(AuxiliaryMeansDto auxiliaryMeansDto)
        {
            var auxiliaryMeans = _mapper.Map<Domain.Entities.AuxiliaryMeans>(auxiliaryMeansDto);
            var savedAuxiliaryMeans = await _auxiliaryMeansRepository.CreateAsync(auxiliaryMeans);
            return _mapper.Map<AuxiliaryMeansDto>(savedAuxiliaryMeans);
        }

        public async Task DeleteAuxiliaryMeansByIdAsync(int auxiliaryMeansId)
        {
            await _auxiliaryMeansRepository.DeleteByIdAsync(auxiliaryMeansId);
        }

        public async Task<IEnumerable<AuxiliaryMeansDto>> ListAuxiliaryMeansAsync()
        {
            var auxiliaryMeansList = await _auxiliaryMeansRepository.ListAsync();
            var list = auxiliaryMeansList.ToList();
            List<AuxiliaryMeansDto> auxiliaryMeansDtos = new();
            for (int i = 0; i < auxiliaryMeansList.Count(); i++)
            {
                auxiliaryMeansDtos.Add(_mapper.Map<AuxiliaryMeansDto>(list[i]));
            }

            return auxiliaryMeansDtos;
        }

        public async Task<AuxiliaryMeansDto> UpdateAuxiliaryMeansAsync(AuxiliaryMeansDto auxiliaryMeansDto)
        {
            var auxiliaryMeans = _auxiliaryMeansRepository.GetById(auxiliaryMeansDto.IdMean); 
            _mapper.Map(auxiliaryMeansDto, auxiliaryMeans);
            await _auxiliaryMeansRepository.UpdateAsync(auxiliaryMeans);
            return _mapper.Map<AuxiliaryMeansDto>(auxiliaryMeans);
        }
    }
}
