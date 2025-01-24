﻿using AutoMapper;
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
    public class TechnologicalMeansService : ITechnologicalMeansService
    {
        private readonly ITechnologicalMeansRepository _technologicalMeansRepository;
        private readonly IMapper _mapper;

        public TechnologicalMeansService(ITechnologicalMeansRepository technologicalMeansRepository, IMapper mapper)
        {
            _technologicalMeansRepository = technologicalMeansRepository;
            _mapper = mapper;
        }

        public async Task<TechnologicalMeansDto> CreateTechnologicalMeansAsync(TechnologicalMeansDto technologicalMeansDto)
        {
            var technologicalMeans = _mapper.Map<Domain.Entities.TechnologicalMeans>(technologicalMeansDto);
            var savedTechnologicalMeans = await _technologicalMeansRepository.CreateAsync(technologicalMeans);
            return _mapper.Map<TechnologicalMeansDto>(savedTechnologicalMeans);
        }

        public async Task<TechnologicalMeansDto> DeleteTechnologicalMeansByIdAsync(int technologicalMeansId)
        {
            var technologicalMean = _technologicalMeansRepository.GetById(technologicalMeansId);
            if (technologicalMean.isDeleted) return null;
            technologicalMean.isDeleted = true;
            await _technologicalMeansRepository.UpdateAsync(technologicalMean);
            return _mapper.Map<TechnologicalMeansDto>(technologicalMean);
        }

        public async Task<IEnumerable<TechnologicalMeansDto>> ListTechnologicalMeansAsync()
        {
            var technologicalMeansList = await _technologicalMeansRepository.ListAsync();
            var list = technologicalMeansList.ToList();
            List<TechnologicalMeansDto> technologicalMeansDtos = new();
            for (int i = 0; i < technologicalMeansList.Count(); i++)
            {
                if(!list[i].isDeleted) technologicalMeansDtos.Add(_mapper.Map<TechnologicalMeansDto>(list[i]));
            }

            return technologicalMeansDtos;
        }

        public async Task<TechnologicalMeansDto> UpdateTechnologicalMeansAsync(TechnologicalMeansDto technologicalMeansDto)
        {
            var technologicalMeans =  _technologicalMeansRepository.GetById(technologicalMeansDto.IdMean);
            if (technologicalMeans.isDeleted) return null;
            _mapper.Map(technologicalMeansDto, technologicalMeans);
            await _technologicalMeansRepository.UpdateAsync(technologicalMeans);
            return _mapper.Map<TechnologicalMeansDto>(technologicalMeans);
        }
    }
}
