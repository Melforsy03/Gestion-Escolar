using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.TechnologicalMeans;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class TechnologicalMeansService : ITechnologicalMeansService
    {
        private readonly ITechnologicalMeansRepository _technologicalMeansRepository;
        private readonly IMapper _mapper;

        public TechnologicalMeansService(ITechnologicalMeansRepository technologicalMeansRepository, IMapper mapper)
        {
            _technologicalMeansRepository = technologicalMeansRepository;
            _mapper = mapper;
        }

        public async Task<TechnologicalMeansResponseDto> CreateTechnologicalMeansAsync(TechnologicalMeansDto technologicalMeansDto)
        {
            var technologicalMeans = _mapper.Map<Domain.Entities.TechnologicalMeans>(technologicalMeansDto);
            var savedTechnologicalMeans = await _technologicalMeansRepository.CreateAsync(technologicalMeans);
            return _mapper.Map<TechnologicalMeansResponseDto>(savedTechnologicalMeans);
        }

        public async Task<TechnologicalMeansResponseDto> DeleteTechnologicalMeansByIdAsync(int technologicalMeansId)
        {
            var technologicalMean = _technologicalMeansRepository.GetById(technologicalMeansId);
            if (technologicalMean.isDeleted) return null;
            technologicalMean.isDeleted = true;
            await _technologicalMeansRepository.UpdateAsync(technologicalMean);
            return _mapper.Map<TechnologicalMeansResponseDto>(technologicalMean);
        }

        public async Task<IEnumerable<TechnologicalMeansResponseDto>> ListTechnologicalMeansAsync()
        {
            var technologicalMeansList = await _technologicalMeansRepository.ListAsync();
            var list = technologicalMeansList.ToList();
            List<TechnologicalMeansResponseDto> technologicalMeansDtos = new();
            for (int i = 0; i < technologicalMeansList.Count(); i++)
            {
                if(!list[i].isDeleted) technologicalMeansDtos.Add(_mapper.Map<TechnologicalMeansResponseDto>(list[i]));
            }

            return technologicalMeansDtos;
        }

        public async Task<TechnologicalMeansResponseDto> UpdateTechnologicalMeansAsync(TechnologicalMeansResponseDto technologicalMeansDto)
        {
            var technologicalMeans =  _technologicalMeansRepository.GetById(technologicalMeansDto.IdMean);
            if (technologicalMeans.isDeleted) return null;
            _mapper.Map(technologicalMeansDto, technologicalMeans);
            await _technologicalMeansRepository.UpdateAsync(technologicalMeans);
            return _mapper.Map<TechnologicalMeansResponseDto>(technologicalMeans);
        }
    }
}
