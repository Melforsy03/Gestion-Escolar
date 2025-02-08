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
    // Clase que implementa los servicios relacionados con los medios auxiliares
    public class AuxiliaryMeansService : IAuxiliaryMeansService
    {
        private readonly IAuxiliaryMeansRepository _auxiliaryMeansRepository; // Repositorio para acceder a los medios auxiliares
        private readonly IMapper _mapper; // Mapper para convertir entre DTOs y entidades

        // Constructor que inyecta las dependencias necesarias
        public AuxiliaryMeansService(IAuxiliaryMeansRepository auxiliaryMeansRepository, IMapper mapper)
        {
            _auxiliaryMeansRepository = auxiliaryMeansRepository;
            _mapper = mapper;
        }

        // Método para crear un nuevo medio auxiliar
        public async Task<AuxiliaryMeansResponseDto> CreateAuxiliaryMeansAsync(AuxiliaryMeansDto auxiliaryMeansDto)
        {
            // Mapea el DTO a la entidad de dominio
            var auxiliaryMeans = _mapper.Map<Domain.Entities.AuxiliaryMeans>(auxiliaryMeansDto);
            // Guarda el nuevo medio auxiliar en la base de datos
            var savedAuxiliaryMeans = await _auxiliaryMeansRepository.CreateAsync(auxiliaryMeans);
            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<AuxiliaryMeansResponseDto>(savedAuxiliaryMeans);
        }

        // Método para eliminar un medio auxiliar por su ID
        public async Task<AuxiliaryMeansResponseDto> DeleteAuxiliaryMeansByIdAsync(int auxiliaryMeansId)
        {
            // Obtiene el medio auxiliar por su ID
            var auxiliaryMean = _auxiliaryMeansRepository.GetById(auxiliaryMeansId);
            // Verifica si el medio auxiliar ya ha sido eliminado
            if (auxiliaryMean.isDeleted)
            {
                return null; // Retorna null si ya está eliminado
            }
            // Marca el medio auxiliar como eliminado
            auxiliaryMean.isDeleted = true;
            // Actualiza el estado en la base de datos
            await _auxiliaryMeansRepository.UpdateAsync(auxiliaryMean);
            // Mapea y retorna el medio auxiliar eliminado como DTO
            return _mapper.Map<AuxiliaryMeansResponseDto>(auxiliaryMean);
        }

        // Método para listar todos los medios auxiliares no eliminados
        public async Task<IEnumerable<AuxiliaryMeansResponseDto>> ListAuxiliaryMeansAsync()
        {
            // Obtiene la lista de medios auxiliares desde el repositorio
            var auxiliaryMeansList = await _auxiliaryMeansRepository.ListAsync();
            var list = auxiliaryMeansList.ToList(); // Convierte a lista para su manipulación
            List<AuxiliaryMeansResponseDto> auxiliaryMeansDtos = new(); // Lista para almacenar los DTOs resultantes

            // Itera sobre la lista de medios auxiliares y agrega solo los no eliminados a la lista de DTOs
            for (int i = 0; i < auxiliaryMeansList.Count(); i++)
            {
                if (!list[i].isDeleted)
                {
                    auxiliaryMeansDtos.Add(_mapper.Map<AuxiliaryMeansResponseDto>(list[i]));
                }
            }

            return auxiliaryMeansDtos; // Retorna la lista de DTOs de medios auxiliares no eliminados
        }

        // Método para actualizar un medio auxiliar existente
        public async Task<AuxiliaryMeansResponseDto> UpdateAuxiliaryMeansAsync(AuxiliaryMeansResponseDto auxiliaryMeansDto)
        {
            // Obtiene el medio auxiliar por su ID desde el DTO
            var auxiliaryMeans = _auxiliaryMeansRepository.GetById(auxiliaryMeansDto.IdMean);
            // Verifica si el medio auxiliar ha sido eliminado antes de proceder a actualizarlo
            if (auxiliaryMeans.isDeleted) return null;

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(auxiliaryMeansDto, auxiliaryMeans);
            // Actualiza la entidad en la base de datos
            await _auxiliaryMeansRepository.UpdateAsync(auxiliaryMeans);
            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<AuxiliaryMeansResponseDto>(auxiliaryMeans);
        }
    }
}
