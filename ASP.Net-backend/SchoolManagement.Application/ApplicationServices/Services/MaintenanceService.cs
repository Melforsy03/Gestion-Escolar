using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Maintenance;
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
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IAuxiliaryMeansRepository _auxiliaryMeansRepository;
        private readonly ITechnologicalMeansRepository _technologicalMeansRepository;
        private readonly IMapper _mapper;

        public MaintenanceService(IMaintenanceRepository maintenanceRepository, ITechnologicalMeansRepository technologicalMeansRepository, IAuxiliaryMeansRepository auxiliaryMeansRepository, IMapper mapper)
        {
            _maintenanceRepository = maintenanceRepository;
            _auxiliaryMeansRepository = auxiliaryMeansRepository;
            _technologicalMeansRepository = technologicalMeansRepository;
            _mapper = mapper;
        }

        public async Task<MaintenanceResponseDto> CreateMaintenanceAsync(MaintenanceDto maintenanceDto)
        {
            var maintenance = _mapper.Map<Domain.Entities.Maintenance>(maintenanceDto);
            if (maintenance.typeOfMean == 0) maintenance.technologicalMean = await _technologicalMeansRepository.GetByIdAsync(maintenance.IdTechMean);
            else maintenance.auxMean = await _auxiliaryMeansRepository.GetByIdAsync(maintenance.IdTechMean);

            var savedMaintenance = await _maintenanceRepository.CreateAsync(maintenance);
            return _mapper.Map<MaintenanceResponseDto>(savedMaintenance);
        }

        public async Task<MaintenanceResponseDto> DeleteMaintenanceByIdAsync(int maintenanceId)
        {
            var maintenance = _maintenanceRepository.GetById(maintenanceId);
            if (maintenance.IsDeleted) return null;
            maintenance.IsDeleted = true;
            await _maintenanceRepository.UpdateAsync(maintenance);
            return _mapper.Map<MaintenanceResponseDto>(maintenance);    
        }

        public async Task<IEnumerable<MaintenanceResponseDto>> ListMaintenancesAsync()
        {
            var maintenances = await _maintenanceRepository.ListAsync();
            var list = maintenances.ToList();
            List<MaintenanceResponseDto> maintenancesList = new();
            for (int i = 0; i < maintenances.Count(); i++)
            {
                if(!list[i].IsDeleted) maintenancesList.Add(_mapper.Map<MaintenanceResponseDto>(list[i]));
            }

            return maintenancesList;
        }

        public async Task<MaintenanceResponseDto> UpdateMaintenanceAsync(MaintenanceResponseDto maintenanceDto)
        {
            var maintenance = _maintenanceRepository.GetById(maintenanceDto.IdM);
            if (maintenance.IsDeleted) return null;
            _mapper.Map(maintenanceDto, maintenance);
            await _maintenanceRepository.UpdateAsync(maintenance);
            return _mapper.Map<MaintenanceResponseDto>(maintenance);
        }
    }
}
