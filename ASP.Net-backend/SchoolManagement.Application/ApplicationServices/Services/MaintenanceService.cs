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
            string name = ""; // Variable para almacenar el nombre del medio asociado
                              // Mapea el DTO de mantenimiento a la entidad de dominio Maintenance
            var maintenance = _mapper.Map<Domain.Entities.Maintenance>(maintenanceDto);

            // Verifica el tipo de medio asociado al mantenimiento
            if (maintenance.typeOfMean == 0) // Si es un medio tecnológico
            {
                // Obtiene el medio tecnológico correspondiente usando el repositorio
                maintenance.technologicalMean = await _technologicalMeansRepository.GetByIdAsync(maintenance.IdTechMean);

                // Verifica si el medio tecnológico existe
                if (maintenance.technologicalMean == null) return null; // Retorna null si no existe

                maintenance.IdTechMean = maintenance.technologicalMean.IdMean; // Asigna el ID del medio tecnológico
                name = maintenance.technologicalMean.NameMean; // Almacena el nombre del medio tecnológico
            }
            else // Si es un medio auxiliar
            {
                // Obtiene el medio auxiliar correspondiente usando el repositorio
                maintenance.auxMean = await _auxiliaryMeansRepository.GetByIdAsync(maintenance.IdAuxMean);

                // Verifica si el medio auxiliar existe
                if (maintenance.auxMean == null) return null; // Retorna null si no existe

                maintenance.IdAuxMean = maintenance.auxMean.IdMean; // Asigna el ID del medio auxiliar
                name = maintenance.auxMean.NameMean; // Almacena el nombre del medio auxiliar
            }

            // Crea el mantenimiento en la base de datos y guarda el resultado
            var savedMaintenance = await _maintenanceRepository.CreateAsync(maintenance);

            // Mapea la entidad guardada de vuelta a un DTO
            var answer = _mapper.Map<MaintenanceResponseDto>(savedMaintenance);

            answer.meanName = name; // Asigna el nombre del medio al DTO de respuesta
            return answer; // Retorna el DTO de mantenimiento creado
        }

        public async Task<MaintenanceResponseDto> DeleteMaintenanceByIdAsync(int maintenanceId)
        {
            // Obtiene el mantenimiento por su ID
            var maintenance = _maintenanceRepository.GetById(maintenanceId);

            // Verifica si el mantenimiento ya está marcado como eliminado
            if (maintenance.IsDeleted) return null;

            // Marca el mantenimiento como eliminado
            maintenance.IsDeleted = true;

            // Actualiza el estado en la base de datos
            await _maintenanceRepository.UpdateAsync(maintenance);

            // Mapea y retorna el mantenimiento eliminado como DTO
            return _mapper.Map<MaintenanceResponseDto>(maintenance);
        }

        public async Task<IEnumerable<MaintenanceResponseDto>> ListMaintenancesAsync()
        {
            // Obtiene todos los mantenimientos desde el repositorio
            var maintenances = await _maintenanceRepository.ListAsync();

            var list = maintenances.ToList(); // Convierte a lista para su manipulación
            List<MaintenanceResponseDto> maintenancesList = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada mantenimiento y agrega solo los no eliminados a la lista de resultados
            for (int i = 0; i < maintenances.Count(); i++)
            {
                if (!list[i].IsDeleted)
                {
                    var maintenance = _mapper.Map<MaintenanceResponseDto>(list[i]); // Mapea a DTO

                    // Asigna el nombre del medio correspondiente según su tipo
                    if (maintenance.typeOfMean == 0)
                        maintenance.meanName = list[i].technologicalMean.NameMean;
                    else
                        maintenance.meanName = list[i].auxMean.NameMean;

                    maintenancesList.Add(maintenance); // Agrega a la lista de resultados
                }
            }

            return maintenancesList; // Retorna la lista de DTOs de mantenimientos no eliminados
        }

        public async Task<MaintenanceResponseDto> UpdateMaintenanceAsync(MaintenanceResponseDto maintenanceDto)
        {
            // Obtiene el mantenimiento por su ID desde el DTO
            var maintenance = _maintenanceRepository.GetById(maintenanceDto.IdM);

            // Verifica si el mantenimiento está marcado como eliminado
            if (maintenance.IsDeleted) return null;

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(maintenanceDto, maintenance);

            // Actualiza la entidad en la base de datos
            await _maintenanceRepository.UpdateAsync(maintenance);

            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<MaintenanceResponseDto>(maintenance);
        }

    }
}
