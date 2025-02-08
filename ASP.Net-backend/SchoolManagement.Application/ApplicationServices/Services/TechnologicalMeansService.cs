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
            // Mapea el DTO de medios tecnológicos a la entidad de dominio TechnologicalMeans
            var technologicalMeans = _mapper.Map<Domain.Entities.TechnologicalMeans>(technologicalMeansDto);

            // Crea el nuevo medio tecnológico en la base de datos
            var savedTechnologicalMeans = await _technologicalMeansRepository.CreateAsync(technologicalMeans);

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<TechnologicalMeansResponseDto>(savedTechnologicalMeans);
        }

        public async Task<TechnologicalMeansResponseDto> DeleteTechnologicalMeansByIdAsync(int technologicalMeansId)
        {
            // Obtiene el medio tecnológico por su ID
            var technologicalMean = _technologicalMeansRepository.GetById(technologicalMeansId);

            // Verifica si el medio tecnológico ya está marcado como eliminado
            if (technologicalMean.isDeleted) return null;

            // Marca el medio tecnológico como eliminado
            technologicalMean.isDeleted = true;

            // Actualiza el estado en la base de datos
            await _technologicalMeansRepository.UpdateAsync(technologicalMean);

            // Mapea y retorna el DTO del medio tecnológico eliminado
            return _mapper.Map<TechnologicalMeansResponseDto>(technologicalMean);
        }

        public async Task<IEnumerable<TechnologicalMeansResponseDto>> ListTechnologicalMeansAsync()
        {
            // Obtiene todos los medios tecnológicos desde el repositorio
            var technologicalMeansList = await _technologicalMeansRepository.ListAsync();

            var list = technologicalMeansList.ToList(); // Convierte a lista para su manipulación
            List<TechnologicalMeansResponseDto> technologicalMeansDtos = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada medio tecnológico y agrega solo los no eliminados a la lista de resultados
            for (int i = 0; i < technologicalMeansList.Count(); i++)
            {
                if (!list[i].isDeleted)
                    technologicalMeansDtos.Add(_mapper.Map<TechnologicalMeansResponseDto>(list[i])); // Mapea y agrega a la lista
            }

            return technologicalMeansDtos; // Retorna la lista de DTOs de medios tecnológicos no eliminados
        }

        public async Task<TechnologicalMeansResponseDto> UpdateTechnologicalMeansAsync(TechnologicalMeansResponseDto technologicalMeansDto)
        {
            // Obtiene el medio tecnológico por su ID desde el DTO
            var technologicalMeans = _technologicalMeansRepository.GetById(technologicalMeansDto.IdMean);

            // Verifica si el medio tecnológico está marcado como eliminado
            if (technologicalMeans.isDeleted) return null;

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(technologicalMeansDto, technologicalMeans);

            // Actualiza la entidad en la base de datos
            await _technologicalMeansRepository.UpdateAsync(technologicalMeans);

            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<TechnologicalMeansResponseDto>(technologicalMeans);
        }

    }
}
